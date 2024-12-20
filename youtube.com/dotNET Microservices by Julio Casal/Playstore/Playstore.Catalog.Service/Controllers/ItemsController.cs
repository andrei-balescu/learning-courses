using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playstore.Catalog.Contracts.DataTransferObjects;
using Playstore.Catalog.Contracts.MassTransit;
using Playstore.Catalog.Service.Entities;
using Playstore.Common;

namespace Playstore.Catalog.Service.Controllers;

/// <summary>Controller for working with items.</summary>
[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    /// <summary>Repository for storing items.</summary>
    private readonly IRepository<Item> _itemsRepository;

    /// <summary>Endpoint for publishing to a message queue.</summary>
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>Create a new instance.</summary>
    /// <param name="itemsRepository">Repository for storing items.</param>
    /// <param name="publishEndpoint">Endpoint for publishing to a message queue.</param>
    public ItemsController(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint)
    {
        _itemsRepository = itemsRepository;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>Returns all items.</summary>
    /// <returns>A list of items</returns>
    /// <remarks>GET /items</remarks>
    [Authorize(Roles = "GameMaster,Player")]
    [HttpGet]
    public async Task<IEnumerable<CatalogItemDto>> GetAsync()
    {
        IEnumerable<CatalogItemDto> items = (await _itemsRepository.GetAllAsync())
                                                            .Select(i => i.AsDto());
        return items;
    }

    /// <summary>Gets a single item</summary>
    /// <param name="id">The <see cref="Guid"/> of the item</param>
    /// <returns>An item DTO.</returns>
    /// <remarks>GET /items/{id}</remarks>
    [Authorize(Roles = "GameMaster,Player")]
    [HttpGet("{id}")]
    public async Task<ActionResult<CatalogItemDto>> GetByIdAsync(Guid id)
    {
        Item? item = await _itemsRepository.GetAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return item.AsDto();
    }

    /// <summary>Create a new item</summary>
    /// <param name="createItemDto">Properties of the item to create</param>
    /// <returns>The result of the action.</returns>
    /// <remarks>POST /items</remarks>
    [Authorize(Roles = "GameMaster")]
    [HttpPost]
    public async Task<ActionResult<CatalogItemDto>> CreateAsync(CreateCatalogItemDto createItemDto)
    {
        var item = new Item
        {
            Id = Guid.NewGuid(),
            Name = createItemDto.Name,
            Description = createItemDto.Description,
            Price = createItemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _itemsRepository.CreateAsync(item);

        await _publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

        return CreatedAtAction(nameof(GetByIdAsync), new { Id = item.Id }, item.AsDto());
    }

    /// <summary>Update an existing item.</summary>
    /// <param name="id">The id of the item to update.</param>
    /// <param name="updateItemDto">The properties that need to be updated.</param>
    /// <returns>No content if item updated; Not found if item not found.</returns>
    /// <remarks>PUT /items/{id}</remarks>
    [Authorize(Roles = "GameMaster")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCatalogItemDto updateItemDto)
    {
        Item? existingItem = await _itemsRepository.GetAsync(id);
        
        if (existingItem == null)
        {
            return NotFound();
        }

        existingItem.Name = updateItemDto.Name;
        existingItem.Description = updateItemDto.Description;
        existingItem.Price = updateItemDto.Price;

        await _itemsRepository.UpdateAsync(existingItem);

        await _publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

        return NoContent();
    }

    /// <summary>Delete an item.</summary>
    /// <param name="id"></param>
    /// <returns>No content if item deleted; Not found if item not found.</returns>
    /// <remarks>DELETE /items/{id}</remarks>
    [Authorize(Roles = "GameMaster")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        Item? existingItem = await _itemsRepository.GetAsync(id);
        
        if (existingItem == null)
        {
            return NotFound();
        }

        await _itemsRepository.RemoveAsync(id);

        await _publishEndpoint.Publish(new CatalogItemDeleted(id));

        return NoContent();
    }
}