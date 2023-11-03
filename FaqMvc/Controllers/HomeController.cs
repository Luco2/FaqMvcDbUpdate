using FaqMvc.Data; 
using GptWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GptWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext; // Create a field for the dbContext

        // Inject the dbContext into the controller's constructor
        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                WelcomeMessage = "Your Welcome Message Here",
                UserPrompts = _dbContext.UserPrompts.ToList()
            };

            return View(viewModel);
        }
    }
}
