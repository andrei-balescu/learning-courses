using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    
}