using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Playstore.Auth.Respositories;

/// <summary>DB context for this application.</summary>
public interface IAppDbContext
{
    /// <summary>A list of registered users.</summary>
    DbSet<IdentityUser> Users { get; set; }
}