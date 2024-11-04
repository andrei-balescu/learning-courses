using System.ComponentModel.DataAnnotations;
using Playstore.Client.Models.Catalog;

namespace Playstore.Client.Models.Inventory;

/// <summary>View model for purchasing items.</summary>
public class PurchaseItemViewModel : CatalogItemViewModel
{
    /// <summary>How many items to purchase.</summary>
    [Range(1, 100)]
    public int Quantity { get; set; }
}