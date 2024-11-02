using MassTransit;
using Playstore.Catalog.Contracts;
using Playstore.Common;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Service.Consumers;

public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
{
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    public CatalogItemDeletedConsumer(IRepository<CatalogItem> catalogItemRepository)
    {
        _catalogItemRepository = catalogItemRepository;
    }

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