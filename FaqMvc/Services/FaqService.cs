using FaqMvc.Data;
using GptWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GptWeb.Services
{
    public class FaqService : IFaqService
    {
        private readonly AppDbContext _dbContext;

        public FaqService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Faq>> GetAllFaqsAsync()
        {
            return await _dbContext.Faqs.ToListAsync();
        }

        public async Task CreateFaqAsync(Faq newFaq)
        {
            _dbContext.Faqs.Add(newFaq);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Faq> GetFaqAsync(int id)
        {
            return await _dbContext.Faqs.FindAsync(id);
        }

        public async Task UpdateFaqAsync(Faq updatedFaq)
        {
            _dbContext.Entry(updatedFaq).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFaqAsync(int id)
        {
            var faq = await _dbContext.Faqs.FindAsync(id);
            if (faq != null)
            {
                _dbContext.Faqs.Remove(faq);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> FaqExists(int id) 
        {
            return await _dbContext.Faqs.AnyAsync(e => e.Id == id);
        }

    }
}
