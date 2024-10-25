using MongoDB.Driver;
using Playstore.Catalog.Service.Entities;

namespace Playstore.Catalog.Service.Repositories;

public class ItemsRepository
{
    private const string COLLECTION_NAME = "items";

    private readonly IMongoCollection<Item> _dbCollection;

    private readonly FilterDefinitionBuilder<Item> _filterDefinitionBuilder = Builders<Item>.Filter;

    public ItemsRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        IMongoDatabase database = mongoClient.GetDatabase("freecodecamp_microservices_catalog");
        _dbCollection = database.GetCollection<Item>(COLLECTION_NAME);
    }

    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
        IReadOnlyCollection<Item> items = await _dbCollection.Find<Item>(_filterDefinitionBuilder.Empty).ToListAsync();
        return items;
    }

    public async Task<Item> GetAsync(Guid itemId)
    {
        FilterDefinition<Item> filter = _filterDefinitionBuilder.Eq(e => e.Id, itemId);
        var item = await _dbCollection.Find<Item>(filter).SingleOrDefaultAsync();
        return item;
    }

    public async Task CreateAsync(Item entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _dbCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Item entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        FilterDefinition<Item> filter = _filterDefinitionBuilder.Eq(e => e.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        FilterDefinition<Item> filter = _filterDefinitionBuilder.Eq(e => e.Id, id);
        await _dbCollection.DeleteOneAsync(filter);
    }
}