using MassTransit;
using Playstore.Catalog.Contracts.MassTransit;
using Playstore.Common;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Service.Consumers;

public class CatalogItemUpdateConsumer : IConsumer<CatalogItemUpdated>
{
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    public CatalogItemUpdateConsumer(IRepository<CatalogItem> catalogItemRepository)
    {
        _catalogItemRepository = catalogItemRepository;
    }

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