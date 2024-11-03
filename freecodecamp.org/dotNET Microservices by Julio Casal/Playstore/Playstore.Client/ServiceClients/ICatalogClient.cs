using Playstore.Client.Dtos;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessint Catalog service.</summary>
public interface ICatalogClient
{
    /// <summary>Get all items from the catalog</summary>
    /// <returns>A list of items.</returns>
    Task<IReadOnlyCollection<CatalogItemDto>> GetAllItemsAsync();

    /// <summary>Create a new item in the catalog.</summary>
    /// <param name="item">The item to create.</param>
    Task CreateItem(CreateCatalogItemDto item);
}