using Playstore.Common;

namespace Playstore.Inventory.Service.Entities;

/// <summary>Represents an inventory item stored in the database.</summary>
public class InventoryItem : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CatalogItemId { get; set; }

    public int Quantity { get; set; }

    public DateTimeOffset AcquiredDate { get; set; }
}