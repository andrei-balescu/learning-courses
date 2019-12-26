using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("OdeToFood");
            base.OnModelCreating(modelBuilder);
        }
    }   
}