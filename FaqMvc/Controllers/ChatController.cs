using FaqMvc.Data;
using GptWeb.Models;
using GptWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GptWeb.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatService _chatService;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<UserModel> _userManager;  // Updated to UserModel

        public ChatController(ChatService chatService, AppDbContext dbContext, UserManager<UserModel> userManager)
        {
            _chatService = chatService;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Question()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(string prompt)
        {
            var answer = await _chatService.GetResponse(prompt);

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

            return RedirectToAction("History");
        }

        public IActionResult History()
        {
            var questionsAndAnswers = _dbContext.UserPrompts.ToList();
            return View(questionsAndAnswers);
        }
    }
}
