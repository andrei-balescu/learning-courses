using Playstore.Inventory.Service.Dtos;

namespace Playstore.Inventory.Service.Entities;

/// <summary>Extensions for <see cref="InventoryItem"/>>.</summary>
public static class InventoryItemExtensions
{
    /// <summary>Return an <see cref="InventoryItem"/> as <see cref="InventoryItemDto"/>.</summary>
    /// <param name="item">The item to be converted</param>
    /// <returns>The item DTO.</returns>
    public static InventoryItemDto AsDto(this InventoryItem item)
    {
        var itemDto = new InventoryItemDto(item.CatalogItemId, item.Quantity, item.AcquiredDate);
        return itemDto;
    }
}