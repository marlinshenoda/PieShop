using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PieShop.Core.Models;

namespace PieShop.Web.Data
    
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Pie>? Pie { get; set; }
        public DbSet<PieShop.Core.Models.Order>? Order { get; set; }
        public DbSet<PieShop.Core.Models.PieGiftOrder>? PieGiftOrder { get; set; }
    }
}