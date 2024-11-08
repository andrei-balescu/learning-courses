using System.Net;
using Playstore.Auth.Contracts.DataTransferObjects;
using Playstore.Client.Models;
using Playstore.Client.Models.Auth;

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
        LoginResponseDto responseContent = null;

        HttpResponseMessage loginResponse = await _httpClient.PostAsJsonAsync("/auth/login", loginRequestDto);
        if (loginResponse.StatusCode == HttpStatusCode.OK)
        {
            responseContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
        }

        return responseContent;
    }

    /// <summary>Register a user. </summary>
    /// <param name="registerRequestDto">Registration request details.</param>  
    /// <returns>The registered user or an error if any.</returns>
    public async Task<RegisterResponseDto> RegisterUserAsync(RegisterRequestDto registerRequestDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/auth/register", registerRequestDto);

        RegisterResponseDto clientTesponse = new RegisterResponseDto();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            clientTesponse.IsSuccess = true;
            clientTesponse.User = await response.Content.ReadFromJsonAsync<UserDto>();
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            clientTesponse.BadRequest = await response.Content.ReadFromJsonAsync<BadRequestDto>();
        }

        return clientTesponse;
    }
}