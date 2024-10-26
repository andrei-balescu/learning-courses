using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Playstore.Catalog.Service.Settings;

namespace Playstore.Catalog.Service.ServiceRegistration;

public class MongoDbFactory
{
    private IOptions<MongoDbSettings> _options;

    public MongoDbFactory(IOptions<MongoDbSettings> options)
    {
        _options = options;
    }

    public IMongoDatabase CreateMongoDb()
    {
        // Default serializer for GUIDs in MongoDB.
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        var client = new MongoClient(_options.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(_options.Value.Database);
        return database;
    }
}