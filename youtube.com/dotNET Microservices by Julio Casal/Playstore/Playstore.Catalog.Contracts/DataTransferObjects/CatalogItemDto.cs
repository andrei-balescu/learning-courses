namespace Playstore.Catalog.Contracts.DataTransferObjects;

/// <summary>An item in the catalog.</summary>
/// <param name="Id">Unique ID of the item.</param>
/// <param name="Name">Item name.</param>
/// <param name="Description">Item description.</param>
/// <param name="Price">Item price.</param>
/// <param name="CreatedDate">Created date of the item.</param>
public record CatalogItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);