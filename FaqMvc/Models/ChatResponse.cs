namespace GptWeb.Models
{
    public class ChatResponse
    {
        public List<ChatChoice> Choices { get; set; } = new List<ChatChoice>();
    }

    public class ChatChoice
    {
        public string Text { get; set; }
        public string TableName { get; set; }
        public string Data { get; set; } 
    }
}

