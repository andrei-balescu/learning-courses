using Playstore.Client.Services;
using Playstore.Inventory.Contracts.DataTransferObjects;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessing Inventory service.</summary>
public class InventoryClient : AuthorizedServiceClient, IInventoryClient
{
    /// <summary>Endpoint for performing operations with inventory items.</summary>
    private const string c_ItemsEndpoint = "/items";

    /// <summary>Create new instance.</summary>
    /// <param name="httpClient">Client to use for communication.</param>
    /// <param name="tokenStorageService">Service that stores JWT tokens.</param>
    public InventoryClient(HttpClient httpClient, ITokenStorageService tokenStorageService) 
        : base(httpClient, tokenStorageService)
    {
    }

    /// <summary>Get the list of items in a user's inventory.</summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>A list of items.</returns>
    public async Task<IReadOnlyCollection<InventoryItemDto>> GetItemsAsync(Guid userId)
    {
        var url = $"{c_ItemsEndpoint}/{userId}";
        IReadOnlyCollection<InventoryItemDto> items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<InventoryItemDto>>(url);
        return items;
    }
    /// <summary>Grant catalog items to a user.</summary>
    /// <param name="grantItemsDto">Parameters for granting items.</param>
    public async Task GrantInventoryItems(GrantInventoryItemsDto grantItemsDto)
    {
        await _httpClient.PostAsJsonAsync(c_ItemsEndpoint, grantItemsDto);
    }
}