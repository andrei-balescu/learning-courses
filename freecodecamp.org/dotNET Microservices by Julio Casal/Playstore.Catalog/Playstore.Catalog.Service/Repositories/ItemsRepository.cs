using MongoDB.Driver;
using Playstore.Catalog.Service.Entities;

namespace Playstore.Catalog.Service.Repositories;

/// <summary>Repository for working with <see cref="Item"/> entities.</summary>
public class ItemsRepository : IItemsRepository
{
    private const string COLLECTION_NAME = "items";

    private readonly IMongoCollection<Item> _dbCollection;

    private readonly FilterDefinitionBuilder<Item> _filterDefinitionBuilder = Builders<Item>.Filter;

    public ItemsRepository(IMongoDatabase database)
    {
        _dbCollection = database.GetCollection<Item>(COLLECTION_NAME);
    }

    /// <summary>Get all items from the database.</summary>
    /// <returns>A list of items.</returns>
    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
        IReadOnlyCollection<Item> items = await _dbCollection.Find<Item>(_filterDefinitionBuilder.Empty).ToListAsync();
        return items;
    }

    /// <summary>Get an item from the database.</summary>
    /// <param name="itemId">ID of the item to retrieve.</param>
    /// <returns>An item.</returns>
    public async Task<Item> GetAsync(Guid itemId)
    {
        FilterDefinition<Item> filter = _filterDefinitionBuilder.Eq(e => e.Id, itemId);
        var item = await _dbCollection.Find<Item>(filter).SingleOrDefaultAsync();
        return item;
    }

    /// <summary>Add new item to the database.</summary>
    /// <param name="entity">The item to add</param>
    /// <exception cref="ArgumentNullException">Thrown if item is null.</exception>
    public async Task CreateAsync(Item entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _dbCollection.InsertOneAsync(entity);
    }

    /// <summary>Update an item in the database.</summary>
    /// <param name="entity">The item to update.</param>
    /// <exception cref="ArgumentNullException">Thrown if item is null.</exception>
    public async Task UpdateAsync(Item entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        FilterDefinition<Item> filter = _filterDefinitionBuilder.Eq(e => e.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);
    }

    /// <summary>Remove an item from the database.</summary>
    /// <param name="id">ID of the item to remove.</param>
    public async Task RemoveAsync(Guid id)
    {
        FilterDefinition<Item> filter = _filterDefinitionBuilder.Eq(e => e.Id, id);
        await _dbCollection.DeleteOneAsync(filter);
    }
}