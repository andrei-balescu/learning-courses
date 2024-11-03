using Playstore.Client.Dto;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessint Catalog service.</summary>
public interface ICatalogClient
{
    /// <summary>Get all items from the catalog</summary>
    /// <returns>A list of items.</returns>
    Task<IReadOnlyCollection<CatalogItemDto>> GetAllItemsAsync();
}