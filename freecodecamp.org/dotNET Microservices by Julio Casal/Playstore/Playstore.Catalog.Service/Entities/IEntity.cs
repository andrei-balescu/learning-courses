namespace Playstore.Catalog.Service.Entities;

/// <summary>Represents an entity in the database.</summary>
public interface IEntity
{
    /// <summary>The unique identifier for this entity.</summary>
    public Guid Id { get; set; }
}
