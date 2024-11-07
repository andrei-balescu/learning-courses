using Playstore.Auth.Contracts.DataTransferObjects;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for communicating with the auth service.</summary>
public class AuthClient : IAuthClient
{
    /// <summary>Client for communicating with the auth service.</summary>
    private readonly HttpClient _httpClient;

    /// <summary>Create a new instance.</summary>
    /// <param name="httpClient">Client for communicating with the auth service.</param>
    public AuthClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        HttpResponseMessage loginResponse = await _httpClient.PostAsJsonAsync("/login", loginRequestDto);
        LoginResponseDto responseContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();

        return responseContent;
    }

    /// <summary>Register a user. </summary>
    /// <param name="registerRequestDto">Registration request details.</param>
    /// <returns>The registered user or an error if any.</returns>
    public async Task<RegisterResponseDto> RegisterUserAsync(RegisterRequestDto registerRequestDto)
    {
        HttpResponseMessage registerResponse = await _httpClient.PostAsJsonAsync("/user/register", registerRequestDto);
        RegisterResponseDto responseContent = await registerResponse.Content.ReadFromJsonAsync<RegisterResponseDto>();

        return responseContent;
    }
}