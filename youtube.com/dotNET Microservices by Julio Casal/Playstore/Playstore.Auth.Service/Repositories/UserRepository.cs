using Microsoft.AspNetCore.Identity;

namespace Playstore.Auth.Respositories;

/// <summary>Service for managing users.</summary>
public class UserRepository : IUserRepository
{
    private readonly IAppDbContext _dbContext;

    public UserRepository(IAppDbContext dbContext)
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