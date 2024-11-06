using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Service.DataTransferObjects;

namespace Playstore.Auth.Service.Services;

public interface IUserService
{
    Task<IEnumerable<IdentityError>?> RegisterUser(RegisterUserDto registerUserDto);

    Task<IdentityUser?> LoginUser(LoginUserDto user);

    IdentityUser GetUserByName(string userId);
}