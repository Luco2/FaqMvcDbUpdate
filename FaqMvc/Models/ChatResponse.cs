namespace GptWeb.Models
{
    public class ChatResponse
    {
        public List<ChatChoice> Choices { get; set; } = new List<ChatChoice>();
    }

    public class ChatChoice
    {
        public string Text { get; set; }
    }
}
