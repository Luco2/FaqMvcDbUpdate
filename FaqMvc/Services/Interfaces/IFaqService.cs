using GptWeb.Models;

public interface IFaqService
{
    Task<List<Faq>> GetAllFaqsAsync();
    Task CreateFaqAsync(Faq newFaq); // Changed return type from Task<Faq> to Task.
    Task<Faq> GetFaqAsync(int id);
    Task UpdateFaqAsync(Faq updatedFaq);
    Task DeleteFaqAsync(int id);
    Task<bool> FaqExists(int id); // Added method to check if a FAQ exists.
    // Include other methods that the FaqService class will implement.
}
