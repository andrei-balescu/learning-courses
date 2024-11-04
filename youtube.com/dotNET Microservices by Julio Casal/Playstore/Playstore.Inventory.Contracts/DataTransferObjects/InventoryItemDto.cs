namespace Playstore.Inventory.Contracts.DataTransferObjects;

/// <summary>Represents an inventory item.</summary>
/// <param name="CatalogItemId">ID of the item in the catalog.</param>
/// <param name="Name">The name of the item.</param>
/// <param name="Description">The item description.</param>
/// <param name="Quantity">Quantity of items in inventory.</param>
/// <param name="AcquiredDate">Date items acquired.</param>
public record InventoryItemDto(Guid CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);