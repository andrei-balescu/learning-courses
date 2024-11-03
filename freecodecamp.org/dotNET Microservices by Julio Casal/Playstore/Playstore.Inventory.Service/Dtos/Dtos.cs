using System.ComponentModel.DataAnnotations;

namespace Playstore.Inventory.Service.Dtos;

/// <summary>DTO for granting items to a user.</summary>
/// <param name="UserId">ID of the user to grant items to.</param>
/// <param name="CatalogItemId">ID of the item in the catalog.</param>
/// <param name="Quantity">Quantity of items to be granted.</param>
public record GrantItemsDto(
    [Required]
    Guid UserId, 
    
    [Required]
    Guid CatalogItemId, 
    
    [Range(1, 1000)]
    int Quantity);

/// <summary>Represents an inventory item.</summary>
/// <param name="CatalogItemId">ID of the item in the catalog.</param>
/// <param name="Name">The name of the item.</param>
/// <param name="Description">The item description.</param>
/// <param name="Quantity">Quantity of items in inventory.</param>
/// <param name="AcquiredDate">Date items acquired.</param>
public record InventoryItemDto(Guid CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);