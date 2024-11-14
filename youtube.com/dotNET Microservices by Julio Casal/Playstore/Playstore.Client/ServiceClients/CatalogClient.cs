using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Client.Services;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessing Catalog service.</summary>
public class CatalogClient : AuthorizedServiceClient, ICatalogClient
{
    /// <summary>Endpoint for performing operations with catalog items.</summary>
    private static string c_itemsEndpoint = "/items";

    /// <summary>Create new instance.</summary>
    /// <param name="httpClient">Client to use for communication.</param>
    /// <param name="tokenStorageService">Service that stores JWT tokens.</param>
    public CatalogClient(HttpClient httpClient, ITokenStorageService tokenStorageService) 
        : base(httpClient, tokenStorageService)
    {
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
    public async Task<CatalogItemDto> GetItemAsync(Guid id)
    {
        var item = await _httpClient.GetFromJsonAsync<CatalogItemDto>($"{c_itemsEndpoint}/{id}");
        return item;
    }

    /// <summary>Create a new item in the catalog.</summary>
    /// <param name="item">The item to create.</param>
    public async Task CreateItemAsync(CreateCatalogItemDto item)
    {
        await _httpClient.PostAsJsonAsync(c_itemsEndpoint, item);
    }

    /// <summary>Update an item in the catalog.</summary>
    /// <param name="itemId">ID of item to update.</param>
    /// <param name="item">The item to update.</param>
    public async Task UpdateItemAsync(Guid itemId, UpdateCatalogItemDto item)
    {
        await _httpClient.PutAsJsonAsync($"{c_itemsEndpoint}/{itemId}", item);
    }

    /// <summary>Delete an item from the catalog.</summary>
    /// <param name="itemId">ID of the item to delete.</param>
    public async Task DeleteItemAsync(Guid itemId)
    {
        await _httpClient.DeleteAsync($"{c_itemsEndpoint}/{itemId}");
    }
}