using System.ComponentModel.DataAnnotations;

namespace Playstore.Client.Models;

public class CatalogItemViewModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    [Range(0, ushort.MaxValue)]
    public decimal Price { get; set; }
}