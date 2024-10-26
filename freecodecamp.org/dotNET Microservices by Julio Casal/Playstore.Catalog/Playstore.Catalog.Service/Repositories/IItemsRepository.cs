using Playstore.Catalog.Service.Entities;

namespace Playstore.Catalog.Service.Repositories;

/// <summary>Repository contract for working with <see cref="Item"/> entities.</summary>
public interface IItemsRepository
{
    /// <summary>Get all items from the database.</summary>
    /// <returns>A list of items.</returns>
    Task<IReadOnlyCollection<Item>> GetAllAsync();

    /// <summary>Get an item from the database.</summary>
    /// <param name="itemId">ID of the item to retrieve.</param>
    /// <returns>An item.</returns>
    Task<Item> GetAsync(Guid itemId);

    /// <summary>Add new item to the database.</summary>
    /// <param name="entity">The item to add</param>
    /// <exception cref="ArgumentNullException">Thrown if item is null.</exception>
    Task CreateAsync(Item entity);

    /// <summary>Update an item in the database.</summary>
    /// <param name="entity">The item to update.</param>
    /// <exception cref="ArgumentNullException">Thrown if item is null.</exception>
    Task UpdateAsync(Item entity);

    /// <summary>Remove an item from the database.</summary>
    /// <param name="id">ID of the item to remove.</param>
    Task RemoveAsync(Guid id);
}