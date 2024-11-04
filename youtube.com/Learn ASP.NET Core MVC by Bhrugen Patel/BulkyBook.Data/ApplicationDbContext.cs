using BulkyBook.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Data;

/// <summary>Database context for this application.</summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base (dbContextOptions)
    {
        
    }
}