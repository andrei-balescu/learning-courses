using Playstore.Inventory.Contracts.DataTransferObjects;

namespace Playstore.Client.ServiceClients;

/// <summary>Client for accessing Inventory service.</summary>
public interface IInventoryClient
{
    /// <summary>Get the list of items in a user's inventory.</summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>A list of items.</returns>
    Task<IReadOnlyCollection<InventoryItemDto>> GetItemsAsync(Guid userId);
}