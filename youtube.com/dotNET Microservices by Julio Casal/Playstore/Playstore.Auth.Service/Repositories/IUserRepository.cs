using Microsoft.AspNetCore.Identity;

namespace Playstore.Auth.Respositories;

/// <summary>Service for managing users.</summary>
public interface IUserRepository
{
    /// <summary>Gets a user by login name.</summary>
    /// <param name="predicate">The query predicate.</param>
    /// <returns>The user.</returns>
    IdentityUser GetUser(Func<IdentityUser, bool> predicate);
}