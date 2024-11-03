namespace Playstore.Client.Dto;

/// <summary>Represents an item from the Catalog microservice.</summary>
/// <param name="Id">The ID of the item in the catalog.</param>
/// <param name="Name">The name of the item.</param>
/// <param name="Description">The item description.</param>
public record CatalogItemDto(Guid Id, string Name, string Description);