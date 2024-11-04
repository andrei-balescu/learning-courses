using System.ComponentModel.DataAnnotations;

namespace Playstore.Catalog.Contracts.DataTransferObjects;

/// <summary>An item in the catalog.</summary>
/// <param name="Id">Unique ID of the item.</param>
/// <param name="Name">Item name.</param>
/// <param name="Description">Item description.</param>
/// <param name="Price">Item price.</param>
/// <param name="CreatedDate">Created date of the item.</param>
public record CatalogItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);

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