using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Playstore.Auth.Service.Models;

namespace Playstore.Auth.Service.Data;

public class AppDbContext : IdentityDbContext<PlaystoreUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<PlaystoreUser> Users { get; set; }
}