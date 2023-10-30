using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GptWeb.Models
{
    public class UserPrompt
    {
        [Key]
        public long UserPromptId { get; set; }
        public long SysUserId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public DateTime DateAsked { get; set; }
    }


}
