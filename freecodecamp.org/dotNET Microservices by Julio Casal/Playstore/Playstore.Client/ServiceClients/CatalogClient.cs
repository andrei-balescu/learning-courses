using MongoDB.Bson;
using Playstore.Client.Dtos;
using Playstore.Client.Dtos;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessint Catalog service.</summary>
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

    /// <summary>Create a new item in the catalog.</summary>
    /// <param name="item">The item to create.</param>
    public async Task CreateItem(CreateCatalogItemDto item)
    {
        await _httpClient.PostAsJsonAsync(c_itemsEndpoint, item);
    }
}