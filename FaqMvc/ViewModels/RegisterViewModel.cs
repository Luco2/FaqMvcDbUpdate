using System.ComponentModel.DataAnnotations;
// Add this if you want to use the dropdown list
// using Microsoft.AspNetCore.Mvc.Rendering; 

namespace GptWeb.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Client Type")]
        public string ClientType { get; set; }

        [Required]
        [Display(Name = "Client ID")]
        public long ClientId { get; set; }

        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Groups")]
        public string Groups { get; set; }

        // Foe a dropdown list of client types:
        // public IEnumerable<SelectListItem> ClientTypes { get; set; }
    }
}
