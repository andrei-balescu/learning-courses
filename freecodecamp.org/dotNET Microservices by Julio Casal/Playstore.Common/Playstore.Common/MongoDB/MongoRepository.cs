using System.Linq.Expressions;
using MongoDB.Driver;

namespace Playstore.Common.MongoDB;

/// <summary>Repository for working with <see cref="IEntity"/> objects.</summary>
public class MongoRepository<T> : IRepository<T> where T: IEntity
{
    private readonly IMongoCollection<T> _dbCollection;

    private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _dbCollection = database.GetCollection<T>(collectionName);
    }

    /// <summary>Get all entities from the database.</summary>
    /// <returns>A list of entities.</returns>
    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        IReadOnlyCollection<T> entities = await _dbCollection.Find<T>(_filterDefinitionBuilder.Empty).ToListAsync();
        return entities;
    }

    /// <summary>Get all entities from the database.</summary>
    /// <param name="filter">The filter to use for getting the entities.</param>
    /// <returns>A list of entities.</returns>
    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        IReadOnlyCollection<T> entities = await _dbCollection.Find<T>(filter).ToListAsync();
        return entities;
    }

    /// <summary>Get an entity from the database.</summary>
    /// <param name="entityId">ID of the entity to retrieve.</param>
    /// <returns>An entity.</returns>
     public async Task<T> GetAsync(Guid entityId)
    {
        FilterDefinition<T> filter = _filterDefinitionBuilder.Eq(e => e.Id, entityId);
        var entity = await _dbCollection.Find<T>(filter).SingleOrDefaultAsync();
        return entity;
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        var entity = await _dbCollection.Find<T>(filter).FirstOrDefaultAsync();
        return entity;
    }

    /// <summary>Add new entity to the database.</summary>
    /// <param name="entity">The entity to add</param>
    /// <exception cref="ArgumentNullException">Thrown if entity is null.</exception>
    public async Task CreateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _dbCollection.InsertOneAsync(entity);
    }

    /// <summary>Update an entity in the database.</summary>
    /// <param name="entity">The entity to update.</param>
    /// <exception cref="ArgumentNullException">Thrown if entity is null.</exception>
    public async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        FilterDefinition<T> filter = _filterDefinitionBuilder.Eq(e => e.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);
    }

    /// <summary>Remove an entity from the database.</summary>
    /// <param name="id">ID of the entity to remove.</param>
    public async Task RemoveAsync(Guid id)
    {
        FilterDefinition<T> filter = _filterDefinitionBuilder.Eq(e => e.Id, id);
        await _dbCollection.DeleteOneAsync(filter);
    }
}