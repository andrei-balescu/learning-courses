using Playstore.Inventory.Contracts.DataTransferObjects;

namespace Playstore.Inventory.Service.Entities;

/// <summary>Extensions for <see cref="InventoryItem"/>>.</summary>
public static class InventoryItemExtensions
{
    /// <summary>Return an <see cref="InventoryItem"/> as <see cref="InventoryItemDto"/>.</summary>
    /// <param name="item">The item to be converted.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">The item description.</param>
    /// <returns>The item DTO.</returns>
    public static InventoryItemDto AsDto(this InventoryItem item, string name, string description)
    {
        var itemDto = new InventoryItemDto(item.CatalogItemId, name, description, item.Quantity, item.AcquiredDate);
        return itemDto;
    }
}