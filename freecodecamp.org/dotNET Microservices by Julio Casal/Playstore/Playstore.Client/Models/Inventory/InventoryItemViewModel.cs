namespace Playstore.Client.Models.Inventory;

public class InventoryItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }
}