using Microsoft.AspNetCore.Identity;

namespace GptWeb.Models
{
    public class UserModel : IdentityUser
    {
        public virtual ICollection<UserPrompt> UserPrompts { get; set; }
    }
}
