using Playstore.Inventory.Service.Dtos;

namespace Playstore.Inventory.Service.Clients;

public class CatalogClient
{
    private readonly HttpClient _httpClient;

    public CatalogClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<CatalogItemDto>> GetItems()
    {
        IReadOnlyCollection<CatalogItemDto>? items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>("/items");
        return items;
    }
}