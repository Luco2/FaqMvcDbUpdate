using GptWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GptWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                WelcomeMessage = "Welcome to our Chatbot!"
                // ... populate other properties as needed
            };

            return View(model);
        }
    }

}
