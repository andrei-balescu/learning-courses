using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Playstore.Catalog.Service.Settings;

namespace Playstore.Catalog.Service.ServiceRegistration;

/// <summary>Creates new instances of a Mongo database.</summary>
public class MongoDbFactory
{
    private IOptions<MongoDbSettings> _options;

    /// <summary>Create new factory instance.</summary>
    /// <param name="options">Mongo DB settings.</param>
    public MongoDbFactory(IOptions<MongoDbSettings> options)
    {
        _options = options;
    }

    /// <summary>Create a new instance of Mongo database with the stored configuration.</summary>
    /// <returns>A mongo database.</returns>
    public IMongoDatabase CreateMongoDb()
    {
        // Default serializer for GUIDs in MongoDB.
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        var client = new MongoClient(_options.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(_options.Value.Database);
        return database;
    }
}