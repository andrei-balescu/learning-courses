using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Service.DataTransferObjects;

namespace Playstore.Auth.Service.Services;

public interface IUserService
{
    Task<IEnumerable<IdentityError>?> RegisterUser(RegisterUserRequest registerUserDto);

    Task<IdentityUser?> LoginUser(LoginUserRequest user);

    IdentityUser GetUserByName(string userId);
}