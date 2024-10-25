using Microsoft.AspNetCore.Mvc;
using Playstore.Catalog.Service.Dtos;

namespace Playstore.Catalog.Service.Controllers;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private static readonly IList<ItemDto> Items = new List<ItemDto>
    {
        new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
    };

    /// <summary>Returns all items.</summary>
    /// <returns>A list of items</returns>
    /// <remarks>GET /items</remarks>
    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
        return Items;
    }

    /// <summary>Gets a single item</summary>
    /// <param name="id">The <see cref="Guid"/> of the item</param>
    /// <returns>An item DTO.</returns>
    /// <remarks>GET /items/{id}</remarks>
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetById(Guid id)
    {
        ItemDto? item = Items.SingleOrDefault(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    /// <summary>Create a new item</summary>
    /// <param name="createItemDto">Properties of the item to create</param>
    /// <returns>The result of the action.</returns>
    /// <remarks>POST /items</remarks>
    [HttpPost]
    public ActionResult<ItemDto> Create(CreateItemDto createItemDto)
    {
        var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
        Items.Add(item);

        return CreatedAtAction(nameof(GetById), new { Id = item.Id }, item);
    }

    /// <summary>Update an existing item.</summary>
    /// <param name="id">The id of the item to update.</param>
    /// <param name="updateItemDto">The properties that need to be updated.</param>
    /// <returns>No content if item updated; Not found if item not found.</returns>
    /// <remarks>PUT /items/{id}</remarks>
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, UpdateItemDto updateItemDto)
    {
        ItemDto? existingItem = Items.SingleOrDefault(i => i.Id == id);
        
        if (existingItem == null)
        {
            return NotFound();
        }

        // DTOs (records) are immutable
        var updatedItem = existingItem with
        {
            Name = updateItemDto.Name,
            Description = updateItemDto.Description,
            Price = updateItemDto.Price
        };

        var index = Items.IndexOf(existingItem);
        Items[index] = updatedItem;

        return NoContent();
    }

    /// <summary>Delete an item.</summary>
    /// <param name="id"></param>
    /// <returns>No content if item deleted; Not found if item not found.</returns>
    /// <remarks>DELETE /items/{id}</remarks>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        ItemDto? item = Items.SingleOrDefault(i => i.Id == id);
        
        if (item == null)
        {
            return NotFound();
        }

        Items.Remove(item);

        return NoContent();
    }
}