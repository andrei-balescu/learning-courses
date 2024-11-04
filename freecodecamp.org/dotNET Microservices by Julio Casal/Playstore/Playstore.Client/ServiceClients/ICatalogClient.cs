using Playstore.Catalog.Contracts.DataTransferObjects;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessint Catalog service.</summary>
public interface ICatalogClient
{
    /// <summary>Get all items from the catalog</summary>
    /// <returns>A list of items.</returns>
    Task<IReadOnlyCollection<CatalogItemDto>> GetAllItemsAsync();

    /// <summary>Get an item by id.</summary>
    /// <param name="id">The id of the item.</param>
    /// <returns>The item details.</returns>
    Task<CatalogItemDto> GetItem(Guid id);

    /// <summary>Create a new item in the catalog.</summary>
    /// <param name="item">The item to create.</param>
    Task CreateItem(CreateCatalogItemDto item);

    /// <summary>Update an item in the catalog.</summary>
    /// <param name="itemId">ID of item to update.</param>
    /// <param name="item">The item to update.</param>
    Task UpdateItem(Guid itemId, UpdateCatalogItemDto item);
}