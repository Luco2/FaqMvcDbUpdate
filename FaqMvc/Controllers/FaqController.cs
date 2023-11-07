using FaqMvc.Data;
using GptWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GptWeb.Controllers
{
    public class FaqController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IFaqService _faqService;

        public FaqController(AppDbContext dbContext, IFaqService faqService)
        {
            _dbContext = dbContext;
            _faqService = faqService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                Data = "FAQs",
                UserPrompts = _dbContext.UserPrompts.ToList(), // Make sure this doesn't need to be awaited
                Faqs = await _faqService.GetAllFaqsAsync()
            };

            return View(viewModel);
        }
    }
}
