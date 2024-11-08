namespace Playstore.Common.Settings;

/// <summary>Setings for signing / validating JWT tokens.</summary>
public class JwtSettings
{
    /// <summary>Secret used for signing tokens.</summary>
    public string Secret { get; set; }

    /// <summary>Token issuer.</summary>
    public string Issuer { get; set; }

    /// <summary>Token audience.</summary>
    public string Audience { get; set; }
}