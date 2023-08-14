using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PieShop.Core.Models;

namespace PieShop.Data
    
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Pie>()
                .HasOne(p => p.RecipeInformation)
                .WithOne(i => i.Pie)
                .HasForeignKey<RecipeInformation>(b => b.PieId);

        
        }
        public DbSet<Pie>? Pie { get; set; }
        public DbSet<Order>? Order { get; set; }
        public DbSet<PieGiftOrder>? PieGiftOrder { get; set; }
        public DbSet<ApplicationUser> ApplicationUser => Set<ApplicationUser>();
    }
}