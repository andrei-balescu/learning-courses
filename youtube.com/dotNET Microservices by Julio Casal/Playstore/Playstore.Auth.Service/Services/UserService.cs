using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Service.Data;

namespace Playstore.Auth.Service;

/// <summary>Service for managing users.</summary>
public class UserService : IUserService
{
    private readonly IAppDbContext _dbContext;

    public UserService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>Gets a user by login name.</summary>
    /// <param name="predicate">The query predicate.</param>
    /// <returns>The user.</returns>
    public IdentityUser GetUser(Func<IdentityUser, bool> predicate)
    {
        IdentityUser? user = _dbContext.Users.SingleOrDefault(predicate);
        return user;
    }
}