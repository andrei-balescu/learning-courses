namespace Playstore.Client.Services;

/// <summary>Service that manages JWT tokens.</summary>
public interface ITokenStorageService
{
    /// <summary>Store a token.</summary>
    /// <param name="token">The token to store.</param>
    void Store(string token);

    /// <summary>Get a token.</summary>
    /// <returns>The token or null if none exists</returns>
    string? Get();

    /// <summary>Clear the stored token.</summary>
    void Clear();
}