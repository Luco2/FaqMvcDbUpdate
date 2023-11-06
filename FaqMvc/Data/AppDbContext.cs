using GptWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaqMvc.Data
{
    public class AppDbContext : IdentityDbContext<UserModel> // Changed to UserModel
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserPrompt> UserPrompts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define the foreign key relationship between UserPrompt and UserModel
            builder.Entity<UserPrompt>()
                .HasOne<UserModel>(up => up.User) // Changed to UserModel
                .WithMany(u => u.UserPrompts) // Added the navigation property
                .HasForeignKey(up => up.UserId);
        }
    }
}
