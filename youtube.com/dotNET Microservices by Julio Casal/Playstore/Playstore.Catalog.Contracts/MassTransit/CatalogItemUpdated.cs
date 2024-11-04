namespace Playstore.Catalog.Contracts.MassTransit;

/// <summary>Result of catalog item updated action.</summary>
/// <param name="ItemId">Id of the item.</param>
/// <param name="Name">Item name.</param>
/// <param name="Description">Item description.</param>
public record CatalogItemUpdated(Guid ItemId, string Name, string Description);