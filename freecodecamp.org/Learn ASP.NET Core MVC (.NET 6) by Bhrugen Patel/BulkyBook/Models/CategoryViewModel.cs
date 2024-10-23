using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models;

/// <summary>View model for displaying category data</summary>
public class CategoryViewModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [DisplayName("Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100")]
    public int DisplayOrder { get; set; }
}