using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GptWeb.Models
{
    public class UserPrompt
    {
        [Key]
        public long UserPromptId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; } 
        public DateTime DateAsked { get; set; }
    }
}
