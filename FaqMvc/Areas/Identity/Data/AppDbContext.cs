using GptWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GptWeb.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserPrompt> UserPrompts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserPrompt>().HasKey(up => up.UserPromptId);

            // Define relationship between UserPrompt and IdentityUser
            builder.Entity<UserPrompt>()
                   .HasOne(up => up.User)
                   .WithMany()
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }

    }
}
