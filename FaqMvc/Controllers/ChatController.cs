using FaqMvc.Data;
using GptWeb.Models;
using GptWeb.Services;
using GptWeb.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GptWeb.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<UserModel> _userManager;
        private readonly IMemoryCache _cache;

        public ChatController(IChatService chatService, AppDbContext dbContext, UserManager<UserModel> userManager, IMemoryCache cache)
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

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Forbid(); 
            }

            string answer = await GetFaqResponse(prompt) ?? await GetStructuredResponse(prompt) ?? await GetOpenEndedResponse(prompt);

            var userPrompt = new UserPrompt
            {
                Question = prompt,
                Answer = answer,
                UserId = user.Id,
                User = user,
                DateAsked = DateTime.UtcNow
            };

            _dbContext.UserPrompts.Add(userPrompt);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        private async Task<string> GetStructuredResponse(string prompt)
        {
            var feeInfo = await _dbContext.Fees
                .FirstOrDefaultAsync(f => prompt.Contains(f.ServiceName));

            if (feeInfo != null)
            {
                var answer = $"The fee for {feeInfo.ServiceName} is {feeInfo.Fee} NZD.";
                if (feeInfo.LatePenalty.HasValue)
                {
                    answer += $" The late penalty is {feeInfo.LatePenalty.Value} NZD.";
                }
                if (!string.IsNullOrEmpty(feeInfo.AdditionalDetails))
                {
                    answer += $" {feeInfo.AdditionalDetails}";
                }
                return answer;
            }

            return null; // No structured response found
        }

        private async Task<string> GetOpenEndedResponse(string prompt)
        {
            if (!_cache.TryGetValue(prompt, out string answer))
            {
                answer = await _chatService.GetResponse(prompt);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) 
                };

                _cache.Set(prompt, answer, cacheEntryOptions);
            }

            return answer;
        }

        private async Task<string> GetFaqResponse(string prompt)
        {
            var faq = await _dbContext.Faqs
                        .FirstOrDefaultAsync(f => f.Question.Contains(prompt));

            return faq?.Answer;
        }
    }
}
