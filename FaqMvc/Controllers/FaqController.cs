using Abp.Web.Mvc.Models;
using GptWeb.Models;
using GptWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GptWeb.Controllers
{
    //[Authorize(Roles = "Admin")] 
    public class FaqController : Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        // GET: Faq
        public async Task<IActionResult> FaqList()
        {
            var faqs = await _faqService.GetAllFaqsAsync();
            var viewModel = new HomeViewModel
            {
                Faqs = faqs
            };
            return View(viewModel);
        }

        // GET: Faq/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _faqService.GetFaqAsync(id.Value);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        // GET: Faq/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faq/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Faq faq)
        {
            faq.DateAsked = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _faqService.CreateFaqAsync(faq);
                return RedirectToAction(nameof(FaqList));
            }
            return View(faq);
        }

        // GET: Faq/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _faqService.GetFaqAsync(id.Value);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        // POST: Faq/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _faqService.UpdateFaqAsync(faq);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _faqService.FaqExists(faq.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(FaqList));
            }
            return View(faq);
        }

        // GET: Faq/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _faqService.GetFaqAsync(id.Value);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        // POST: Faq/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _faqService.DeleteFaqAsync(id);
            return RedirectToAction(nameof(FaqList));
        }
        }

}
