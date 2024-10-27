using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Playstore.Common.Settings;

namespace Playstore.Common.MongoDB;

/// <summary>Extenstion methods for registering MongoDB services.</summary>
public static class MongoRegistrationExtensions
{
    /// <summary>Registers a Mongo database.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add this method to.</param>
    /// <returns>An <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        // Default serializer for GUIDs in MongoDB.
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(mongoDbSettings.Database);

            return database;
        });

        return services;
    }

    /// <summary>Registers a MongoDB repository.</summary>
    /// <typeparam name="T">The entity type that the repository manages.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add this method to.</param>
    /// <param name="collectionName">Name of the collection to use.</param>
    /// <returns>An <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
    {
        services.AddScoped<IRepository<T>>(serviceProvider => 
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            var repository = new MongoRepository<T>(database, collectionName);
            return repository;
        });

        return services;
    }
}