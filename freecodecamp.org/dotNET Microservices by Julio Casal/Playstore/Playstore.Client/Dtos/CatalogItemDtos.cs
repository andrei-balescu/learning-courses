namespace Playstore.Client.Dtos;

/// <summary>Represents an item from the Catalog microservice.</summary>
/// <param name="Id">The ID of the item in the catalog.</param>
/// <param name="Name">The name of the item.</param>
/// <param name="Description">The item description.</param>
/// <param name="Price">The price of the item.</param>
public record CatalogItemDto(Guid Id, string Name, string Description, int Price);

/// <summary>Add a new item to the catalog.</summary>
/// <param name="Name">The name of the new item (required).</param>
/// <param name="Description">The description of the new item.</param>
/// <param name="Price">The price of the new item (max 1000).</param>
public record CreateCatalogItemDto(string Name, string Description, decimal Price);