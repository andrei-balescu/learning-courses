using System.ComponentModel.DataAnnotations;

namespace Playstore.Catalog.Contracts.DataTransferObjects;

/// <summary>Update an item in the catalog.</summary>
/// <param name="Name">The new name of the item (required).</param>
/// <param name="Description">The new description of the item.</param>
/// <param name="Price">The new priec of the item (max 1000).</param>
public record UpdateCatalogItemDto(
    [Required] 
    string Name, 

    string Description, 

    [Range(0, 1000)] 
    decimal Price
);