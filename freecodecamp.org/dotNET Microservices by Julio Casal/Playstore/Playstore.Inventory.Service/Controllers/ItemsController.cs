using Microsoft.AspNetCore.Mvc;
using Playstore.Common;
using Playstore.Inventory.Service.Clients;
using Playstore.Inventory.Service.Dtos;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Service.Controllers;

/// <summary>Controller for managing inventory items.</summary>
[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<InventoryItem> _itemsRepository;
    private readonly CatalogClient _catalogClient;

    public ItemsController(IRepository<InventoryItem> itemsRepository, CatalogClient catalogClient)
    {
        _itemsRepository = itemsRepository;
        _catalogClient = catalogClient;
    }

    /// <summary>Retrieves all inventory items for a user.</summary>
    /// <param name="userId">The user to retrieve items for.</param>
    /// <returns>A list of items.</returns>
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest();
        }

        IReadOnlyCollection<CatalogItemDto> catalogItems = await _catalogClient.GetItems();
        IEnumerable<InventoryItem> inventoryItems = await _itemsRepository.GetAllAsync(i => i.UserId == userId);

        IEnumerable<InventoryItemDto> items = 
            from catalogItem in catalogItems
            join inventoryItem in inventoryItems
            on catalogItem.Id equals inventoryItem.CatalogItemId
            select inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);

        return Ok(items);
    }

    /// <summary>Grants items to a user.</summary>
    /// <param name="grantItemsDto">DTO specifying grant details.</param>
    /// <returns>OK response.</returns>
    [HttpPost]
    public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
    {
        InventoryItem inventoryItem = await _itemsRepository.GetAsync(i => i.UserId == grantItemsDto.UserId
                                                            && i.CatalogItemId == grantItemsDto.CatalogItemId);

        if (inventoryItem == null)
        {
            inventoryItem = new InventoryItem
            {
                Id = Guid.NewGuid(),
                UserId = grantItemsDto.UserId,
                CatalogItemId = grantItemsDto.CatalogItemId,
                Quantity = grantItemsDto.Quantity,
                AcquiredDate = DateTimeOffset.UtcNow
            };

            await _itemsRepository.CreateAsync(inventoryItem);
        }
        else
        {
            inventoryItem.Quantity += grantItemsDto.Quantity;

            await _itemsRepository.UpdateAsync(inventoryItem);
        }

        return Ok();
    }
}