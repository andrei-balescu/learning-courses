using Playstore.Common;

namespace Playstore.Catalog.Service.Entities;

/// <summary>Represents an item stored in the database</summary>
public class Item : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}