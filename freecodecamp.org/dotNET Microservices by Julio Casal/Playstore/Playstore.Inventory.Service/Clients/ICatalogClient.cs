using Playstore.Inventory.Service.Dtos;

namespace Playstore.Inventory.Service.Clients;

public interface ICatalogClient
{
    Task<IReadOnlyCollection<CatalogItemDto>> GetItems();
}