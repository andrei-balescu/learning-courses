namespace Playstore.Auth.Service.Settings;

/// <summary>Setings for signing JWT tokens.</summary>
public class JwtSettings
{
    public string Secret { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
}