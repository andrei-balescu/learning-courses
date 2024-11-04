using MassTransit;
using Playstore.Catalog.Contracts.MassTransit;
using Playstore.Common;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Service.Consumers;

/// <summary>Consumer of catalog item updated messages through MassTransit.</summary>
public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
{
    /// <summary>Repository to use for syncing data.</summary>
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    /// <summary>Create new class instance.</summary>
    /// <param name="catalogItemRepository">Repository to use for syncing data.</param>
    public CatalogItemDeletedConsumer(IRepository<CatalogItem> catalogItemRepository)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    /// <summary>Synchronizes local database with the result of the action.</summary>
    /// <param name="context">Action context.</param>
    public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
    {
        CatalogItemDeleted message = context.Message;

        var item = await _catalogItemRepository.GetAsync(message.ItemId);

        if (item != null)
        {
            await _catalogItemRepository.RemoveAsync(item.Id);
        }
    }
}