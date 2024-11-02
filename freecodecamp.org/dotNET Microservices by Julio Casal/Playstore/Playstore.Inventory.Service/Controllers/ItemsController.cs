using Microsoft.AspNetCore.Mvc;
using Playstore.Common;
using Playstore.Inventory.Service.Dtos;
using Playstore.Inventory.Service.Entities;

namespace Playstore.Inventory.Service.Controllers;

/// <summary>Controller for managing inventory items.</summary>
[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<InventoryItem> _inventoryItemsRepository;
    private readonly IRepository<CatalogItem> _catalogItemsRepository;

    public ItemsController(IRepository<InventoryItem> itemsRepository, IRepository<CatalogItem> catalogItemsRepository)
    {
        _inventoryItemsRepository = itemsRepository;
        _catalogItemsRepository = catalogItemsRepository;
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

        IReadOnlyCollection<InventoryItem> inventoryItems = await _inventoryItemsRepository.GetAllAsync(i => i.UserId == userId);

        IEnumerable<Guid> catalogItemIds = inventoryItems.Select(item => item.CatalogItemId);
        IReadOnlyCollection<CatalogItem> catalogItems = await _catalogItemsRepository.GetAllAsync(item => catalogItemIds.Contains(item.Id));


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
        InventoryItem inventoryItem = await _inventoryItemsRepository.GetAsync(i => i.UserId == grantItemsDto.UserId
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

            await _inventoryItemsRepository.CreateAsync(inventoryItem);
        }
        else
        {
            inventoryItem.Quantity += grantItemsDto.Quantity;

            await _inventoryItemsRepository.UpdateAsync(inventoryItem);
        }

        return Ok();
    }
}