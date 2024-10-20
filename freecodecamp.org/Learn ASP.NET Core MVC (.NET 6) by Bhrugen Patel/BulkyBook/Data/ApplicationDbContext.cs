using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }
}