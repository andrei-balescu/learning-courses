using System.ComponentModel.DataAnnotations;

namespace Playstore.Inventory.Contracts.DataTransferObjects;

/// <summary>DTO for granting items to a user.</summary>
/// <param name="UserId">ID of the user to grant items to.</param>
/// <param name="CatalogItemId">ID of the item in the catalog.</param>
/// <param name="Quantity">Quantity of items to be granted.</param>
public record GrantInventoryItemsDto(
    [Required]
    Guid UserId, 
    
    [Required]
    Guid CatalogItemId, 
    
    [Range(1, 1000)]
    int Quantity);