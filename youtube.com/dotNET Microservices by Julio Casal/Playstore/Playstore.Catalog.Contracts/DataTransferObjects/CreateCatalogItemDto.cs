using System.ComponentModel.DataAnnotations;

namespace Playstore.Catalog.Contracts.DataTransferObjects;

/// <summary>Add a new item to the catalog.</summary>
/// <param name="Name">The name of the new item (required).</param>
/// <param name="Description">The description of the new item.</param>
/// <param name="Price">The price of the new item (max 1000).</param>
public record CreateCatalogItemDto(
    [Required] 
    string Name, 

    string Description, 

    [Range(0, 1000)] 
    decimal Price
);