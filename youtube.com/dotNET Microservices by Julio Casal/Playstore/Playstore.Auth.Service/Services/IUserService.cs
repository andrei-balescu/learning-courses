using Microsoft.AspNetCore.Identity;
using Playstore.Auth.Service.DataTransferObjects;

namespace Playstore.Auth.Service.Services;

public interface IUserService
{
    Task<IEnumerable<IdentityError>?> RegisterUser(RegisterRequestDto registerUserDto);

    Task<IdentityUser?> LoginUser(LoginRequestDto user);

    IdentityUser GetUserByName(string userId);
}