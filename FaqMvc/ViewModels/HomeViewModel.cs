using GptWeb.Models;

namespace GptWeb.ViewModels
{
    public class HomeViewModel
    {
        public string Data { get; set; }
        public IEnumerable<UserPrompt> UserPrompts { get; set; }
    }
}
