using GptWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaqMvc.Data
{
    public class AppDbContext : IdentityDbContext<UserModel> // Assuming UserModel is your custom user class
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserPrompt> UserPrompts { get; set; }
        public DbSet<FeeInfo> Fees { get; set; } // Assuming FeeInfo is your custom class for fees

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define the foreign key relationship between UserPrompt and UserModel
            builder.Entity<UserPrompt>()
                .HasOne<UserModel>(up => up.User) // Ensure UserModel is the type of your User class
                .WithMany(u => u.UserPrompts) // Assuming UserPrompts is the navigation property in UserModel
                .HasForeignKey(up => up.UserId);

            // Configure FeeInfo entity
            builder.Entity<FeeInfo>(entity =>
            {
                entity.ToTable("Fees");

                // Specify the store type for the Fee property
                entity.Property(e => e.Fee)
                    .HasColumnType("decimal(18, 2)") // Adjust precision and scale as needed
                    .HasPrecision(18, 2);

                // Specify the store type for the LatePenalty property
                entity.Property(e => e.LatePenalty)
                    .HasColumnType("decimal(18, 2)") // Adjust precision and scale as needed
                    .HasPrecision(18, 2);
            });
        }
    }
}
