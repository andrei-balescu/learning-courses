using Microsoft.AspNetCore.Identity;

namespace Playstore.Auth.Service;

/// <summary>Service for managing users.</summary>
public interface IUserService
{
    /// <summary>Gets a user by login name.</summary>
    /// <param name="predicate">The query predicate.</param>
    /// <returns>The user.</returns>
    IdentityUser GetUser(Func<IdentityUser, bool> predicate);
}