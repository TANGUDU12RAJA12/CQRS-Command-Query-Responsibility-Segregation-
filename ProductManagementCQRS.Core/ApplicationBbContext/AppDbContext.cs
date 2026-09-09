using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.Domain.Entities;
namespace ProductManagementSystem.Core.ApplicationBbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
             
        }

        public DbSet<Product> Product_CQRS { get; set; }
    }
}
