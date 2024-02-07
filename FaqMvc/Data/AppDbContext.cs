using GptWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaqMvc.Data
{
    public class AppDbContext : IdentityDbContext<UserModel> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserPrompt> UserPrompts { get; set; }
        public DbSet<FeeInfo> Fees { get; set; }
        public DbSet<Faq> Faqs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            builder.Entity<UserPrompt>()
                .HasOne<UserModel>(up => up.User) 
                .WithMany(u => u.UserPrompts) 
                .HasForeignKey(up => up.UserId);

            
            builder.Entity<FeeInfo>(entity =>
            {
                entity.ToTable("Fees");

                
                entity.Property(e => e.Fee)
                    .HasColumnType("decimal(18, 2)") 
                    .HasPrecision(18, 2);

                
                entity.Property(e => e.LatePenalty)
                    .HasColumnType("decimal(18, 2)") 
                    .HasPrecision(18, 2);
            });
        }
    }
}
