using FaqMvc.Data;
using GptWeb.Models;
using GptWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GptWeb.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatService _chatService;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<UserModel> _userManager;
        private readonly IMemoryCache _cache;

        public ChatController(ChatService chatService, AppDbContext dbContext, UserManager<UserModel> userManager,IMemoryCache cache)
        {
            _chatService = chatService;
            _dbContext = dbContext;
            _userManager = userManager;
            _cache = cache;
        }

        public IActionResult Question()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(string prompt)
        {
            // Check cache for a previously stored answer
            if (!_cache.TryGetValue(prompt, out string answer))
            {
                // If not in cache, get from API
                answer = await _chatService.GetResponse(prompt);

                // Store in cache with an expiration time (you can adjust this duration)
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };

                _cache.Set(prompt, answer, cacheEntryOptions);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                // Handle the case where the user is not authenticated or user does not exist
                return Forbid(); // Or any other appropriate response
            }

            var userPrompt = new UserPrompt
            {
                Question = prompt,
                Answer = answer,
                UserId = user.Id,
                User = user,  // user is already UserModel type due to UserManager<UserModel>
                DateAsked = DateTime.Now
            };

            _dbContext.UserPrompts.Add(userPrompt);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
