namespace Playstore.Client.Models.Inventory;

/// <summary>View model vor an inventory item.</summary>
public class InventoryItemViewModel
{
    /// <summary>ID of the item in the catalog.</summary>
    public Guid Id { get; set; }

    /// <summary>Item name.</summary>
    public string Name { get; set; }

    /// <summary>Item description.</summary>
    public string Description { get; set; }

    /// <summary>Item quantity.</summary>
    public int Quantity { get; set; }
}