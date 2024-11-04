namespace Playstore.Catalog.Contracts.MassTransit;

/// <summary>Result of catalog item deleted item.</summary>
/// <param name="ItemId">ID of the item.</param>
public record CatalogItemDeleted(Guid ItemId);