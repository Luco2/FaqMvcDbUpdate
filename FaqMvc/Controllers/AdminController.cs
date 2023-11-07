using GptWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GptWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IFaqService _faqService;

        public AdminController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await _faqService.GetAllFaqsAsync();
            return View(faqs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faq faq)
        {
            if (ModelState.IsValid)
            {
                await _faqService.CreateFaqAsync(faq);
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var faq = await _faqService.GetFaqAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _faqService.UpdateFaqAsync(faq);
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var faq = await _faqService.GetFaqAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _faqService.DeleteFaqAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

