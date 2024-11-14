namespace Playstore.Client.Services;

/// <summary>Service that manages JWT tokens.</summary>
public class TokenStorageService : ITokenStorageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string c_tokenCookieName = "jwtToken";

    /// <summary>Create new instance.</summary>
    /// <param name="httpContextAccessor">Accessor fot the <see cref="HttpContext"/>.</param>
    public TokenStorageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>Clear the stored token.</summary>
    public void Clear()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(c_tokenCookieName);
    }
    /// <summary>Get a token.</summary>
    /// <returns>The token or null if none exists</returns>
    public string? Get()
    {
        string? token = null;
        _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(c_tokenCookieName, out token);

        return token;
    }

    // <summary>Store a token.</summary>
    /// <param name="token">The token to store.</param>
    public void Store(string token)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(c_tokenCookieName, token);
    }
}