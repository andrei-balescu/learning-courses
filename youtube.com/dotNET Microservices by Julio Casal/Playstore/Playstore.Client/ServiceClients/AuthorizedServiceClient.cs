using Microsoft.AspNetCore.Authentication.JwtBearer;
using Playstore.Client.Services;

namespace Playstore.Client.ServiceClients;

/// <summary>Client using JWT bearer authorization.</summary>
public abstract class AuthorizedServiceClient
{
    protected readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorageService;

    private const string c_authorizationHeader = "Authorization";

    /// <summary>Create new instance.</summary>
    /// <param name="httpClient">Client to use for communication.</param>
    /// <param name="tokenStorageService">Service that stores JWT tokens.</param>
    protected AuthorizedServiceClient(HttpClient httpClient, ITokenStorageService tokenStorageService)
    {
        _tokenStorageService = tokenStorageService;
        _httpClient = httpClient;

        string? token = _tokenStorageService.Get();
        if (token != null)
        {
            _httpClient.DefaultRequestHeaders.Add(c_authorizationHeader, $"{JwtBearerDefaults.AuthenticationScheme} {token}");
        }
    }
}