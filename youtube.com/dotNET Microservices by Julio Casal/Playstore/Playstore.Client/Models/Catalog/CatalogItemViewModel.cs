using System.ComponentModel.DataAnnotations;

namespace Playstore.Client.Models.Catalog;

/// <summary>View model for catalog items.</summary>
public class CatalogItemViewModel
{
    /// <summary>Item ID.</summary>
    public Guid Id { get; set; }

    /// <summary>Item name.</summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>Item description.</summary>
    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    /// <summary>Item price.</summary>
    [Range(0, 1000)]
    public decimal Price { get; set; }
}