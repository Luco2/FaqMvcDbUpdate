namespace GptWeb.Services.Interfaces
{
    public interface IChatService
    {
        Task<string> GetResponse(string prompt);
    }
}
