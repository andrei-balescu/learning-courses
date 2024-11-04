using System.ComponentModel.DataAnnotations;

namespace Playstore.Client.Models.Catalog;

public class CatalogItemViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    [Range(0, 1000)]
    public decimal Price { get; set; }
}