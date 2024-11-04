using System.Linq.Expressions;

namespace Playstore.Common;

/// <summary>Repository contract for working with <see cref="IEntity"/> entities.</summary>
public interface IRepository<T> where T : IEntity
{
    /// <summary>Get all entities from the database.</summary>
    /// <returns>A list of entities.</returns>
    Task<IReadOnlyCollection<T>> GetAllAsync();

    /// <summary>Get all entities from the database.</summary>
    /// <param name="filter">The filter to use for getting the entities.</param>
    /// <returns>A list of entities.</returns>
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);

    /// <summary>Get an item from the database.</summary>
    /// <param name="entityId">ID of the item to retrieve.</param>
    /// <returns>An entity.</returns>
    Task<T> GetAsync(Guid entityId);

    /// <summary>Get an item from the database.</summary>
    /// <param name="filter">The filter to use for getting the entity.</param>
    /// <returns>An entity.</returns>
    Task<T> GetAsync(Expression<Func<T, bool>> filter);

    /// <summary>Add new entity to the database.</summary>
    /// <param name="entity">The entity to add</param>
    Task CreateAsync(T entity);

    /// <summary>Update an entity in the database.</summary>
    /// <param name="entity">The entity to update.</param>
    Task UpdateAsync(T entity);

    /// <summary>Remove an entity from the database.</summary>
    /// <param name="id">ID of the entity to remove.</param>
    Task RemoveAsync(Guid id);
}