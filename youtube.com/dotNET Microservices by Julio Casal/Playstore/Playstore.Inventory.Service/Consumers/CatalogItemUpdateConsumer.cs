using MassTransit;
using Playstore.Catalog.Contracts.MassTransit;
using Playstore.Common;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Service.Consumers;

/// <summary>Consumer of catalog item deleted messages through MassTransit.</summary>
public class CatalogItemUpdateConsumer : IConsumer<CatalogItemUpdated>
{
    /// <summary>Repository to use for syncing data.</summary>
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    /// <summary>Create new class instance.</summary>
    /// <param name="catalogItemRepository">Repository to use for syncing data.</param>
    public CatalogItemUpdateConsumer(IRepository<CatalogItem> catalogItemRepository)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    /// <summary>Synchronizes local database with the result of the action.</summary>
    /// <param name="context">Action context.</param>
    public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
    {
        CatalogItemUpdated message = context.Message;

        var item = await _catalogItemRepository.GetAsync(message.ItemId);
        if (item != null)
        {
            item.Name = message.Name;
            item.Description = message.Description;
            _catalogItemRepository.UpdateAsync(item);
        }
        else
        {
            item = new CatalogItem
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };
            await _catalogItemRepository.CreateAsync(item);
        }
    }
}