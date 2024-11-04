using Playstore.Catalog.Contracts.DataTransferObjects;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessing Catalog service.</summary>
public class CatalogClient : ICatalogClient
{
    /// <summary>Client to use for communication.</summary>
    private readonly HttpClient _httpClient;

    /// <summary>Endpoint for performing operations with catalog items.</summary>
    private static string c_itemsEndpoint = "/items";

    /// <summary>Create new instance of <see cref="CatalogClient"/>.</summary>
    /// <param name="httpClient">Client to use for communication.</param>
    public CatalogClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>Get all items from the catalog</summary>
    /// <returns>A list of items.</returns>
    public async Task<IReadOnlyCollection<CatalogItemDto>> GetAllItemsAsync()
    {
        var items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>(c_itemsEndpoint);
        return items;
    }

    /// <summary>Get an item by id.</summary>
    /// <param name="id">The id of the item.</param>
    /// <returns>The item details.</returns>
    public async Task<CatalogItemDto> GetItem(Guid id)
    {
        var item = await _httpClient.GetFromJsonAsync<CatalogItemDto>($"{c_itemsEndpoint}/{id}");
        return item;
    }

    /// <summary>Create a new item in the catalog.</summary>
    /// <param name="item">The item to create.</param>
    public async Task CreateItem(CreateCatalogItemDto item)
    {
        await _httpClient.PostAsJsonAsync(c_itemsEndpoint, item);
    }

    /// <summary>Update an item in the catalog.</summary>
    /// <param name="itemId">ID of item to update.</param>
    /// <param name="item">The item to update.</param>
    public async Task UpdateItem(Guid itemId, UpdateCatalogItemDto item)
    {
        await _httpClient.PutAsJsonAsync($"{c_itemsEndpoint}/{itemId}", item);
    }

    /// <summary>Delete an item from the catalog.</summary>
    /// <param name="itemId">ID of the item to delete.</param>
    public async Task DeleteItem(Guid itemId)
    {
        await _httpClient.DeleteAsync($"{c_itemsEndpoint}/{itemId}");
    }
}