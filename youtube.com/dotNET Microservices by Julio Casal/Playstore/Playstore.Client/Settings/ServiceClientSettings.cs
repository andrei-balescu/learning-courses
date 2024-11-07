namespace Playstore.Client.Settings;

/// <summary>Represents settings for communicating with dependent services.</summary>
public class ServiceClientSettings
{
    /// <summary>URL of the catalog service.</summary>
    public string CatalogServiceUrl { get; set; }

    /// <summary>URL of the inventory service.</summary>
    public string InventoryServiceUrl { get; set; }

    public string AuthServiceUrl { get; set; }
}