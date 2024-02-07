using GptWeb.Models;

public interface IFaqService
{
    Task<List<Faq>> GetAllFaqsAsync();
    Task CreateFaqAsync(Faq newFaq); 
    Task<Faq> GetFaqAsync(int id);
    Task UpdateFaqAsync(Faq updatedFaq);
    Task DeleteFaqAsync(int id);
    Task<bool> FaqExists(int id);
}
