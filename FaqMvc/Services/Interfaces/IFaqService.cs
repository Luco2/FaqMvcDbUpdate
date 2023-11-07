using GptWeb.Models;

public interface IFaqService
{
    Task<List<Faq>> GetAllFaqsAsync();
    Task<Faq> CreateFaqAsync(Faq newFaq);
    Task<Faq> GetFaqAsync(int id);
    Task UpdateFaqAsync(Faq updatedFaq);
    Task DeleteFaqAsync(int id);
    // Include other methods that the FaqService class will implement.
}
