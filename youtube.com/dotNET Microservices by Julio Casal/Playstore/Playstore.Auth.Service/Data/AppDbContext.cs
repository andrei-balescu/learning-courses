using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Playstore.Auth.Service.Data;

/// <summary>DB context for this application.</summary>
public class AppDbContext : IdentityDbContext<IdentityUser>
{
    /// <summary>Create a new instance.</summary>
    /// <param name="options">Option for this <see cref="IdentityDbContext"/>.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    /// <summary>A list of registered users.</summary>
    public DbSet<IdentityUser> Users { get; set; }
}