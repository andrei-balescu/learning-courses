using System.ComponentModel.DataAnnotations;

namespace Playstore.Client.Models;

public class CatalogItemViewModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    [Range(0, ushort.MaxValue)]
    public int Price { get; set; }
}