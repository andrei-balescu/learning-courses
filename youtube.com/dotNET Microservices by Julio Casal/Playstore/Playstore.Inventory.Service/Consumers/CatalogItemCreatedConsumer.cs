using MassTransit;
using Playstore.Catalog.Contracts.MassTransit;
using Playstore.Common;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Service.Consumers;

/// <summary>Consumer of catalog item created messages through MassTransit.</summary>
public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
{
    /// <summary>Repository to use for syncing data.</summary>
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    /// <summary>Create new class instance.</summary>
    /// <param name="catalogItemRepository">Repository to use for syncing data.</param>
    public CatalogItemCreatedConsumer(IRepository<CatalogItem> catalogItemRepository)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    /// <summary>Synchronizes local database with the result of the action.</summary>
    /// <param name="context">Action context.</param>
    public async Task Consume(ConsumeContext<CatalogItemCreated> context)
    {
        CatalogItemCreated message = context.Message;

        CatalogItem item = await _catalogItemRepository.GetAsync(message.ItemId);

        if (item == null)
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