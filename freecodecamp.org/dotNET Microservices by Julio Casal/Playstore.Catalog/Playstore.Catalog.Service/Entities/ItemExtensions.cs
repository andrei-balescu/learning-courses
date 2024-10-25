using Playstore.Catalog.Service.Dtos;

namespace Playstore.Catalog.Service.Entities;

public static class DtoExtensions
{
    /// <summary>Create an <see cref="ItemDto"/> from an <see cref="Item"/>.</summary>
    /// <param name="item">The item to create the DTO from.</param>
    /// <returns>The item DTO.</returns>
    public static ItemDto AsDto(this Item item)
    {
        var dto = new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        return dto;
    }
}