using FaqMvc.Data; 
using GptWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace GptWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext; // Create a field for the dbContext
        private readonly IStringLocalizer<HomeController> _localizer;

        // Inject the dbContext into the controller's constructor
        public HomeController(AppDbContext dbContext, IStringLocalizer<HomeController> localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        [Authorize]
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Data = _localizer["Data"], 
                UserPrompts = _dbContext.UserPrompts.ToList()
            };

            return View(viewModel);
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var isValidCulture = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                                             .Any(ci => ci.Name == culture);

            if (!isValidCulture)
            {
                // Handle invalid culture, e.g., set a default culture
                // Redirect to a default action
                return RedirectToAction(nameof(Index)); // Replace 'Index' with your default action
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return RedirectToAction(nameof(Index)); // Replace 'Index' with your default action
            }

            return LocalRedirect(returnUrl);
        }

    }
}
