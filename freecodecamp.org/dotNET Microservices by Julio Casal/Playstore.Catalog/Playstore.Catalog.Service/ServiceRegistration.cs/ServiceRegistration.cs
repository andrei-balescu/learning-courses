using Playstore.Catalog.Service.Repositories;
using Playstore.Catalog.Service.Settings;

namespace Playstore.Catalog.Service.ServiceRegistration;

/// <summary>Registers services with the provided <see cref="IServiceCollection"/>.</summary>
public class ServiceRegistration
{
    private WebApplicationBuilder _builder;

    public ServiceRegistration(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>Adds a mongo database.</summary>
    public void AddMongoDb()
    {
        _builder.Services.Configure<MongoDbSettings>(_builder.Configuration.GetSection(nameof(MongoDbSettings)));
        // required for injecting the MongoDbSettings.
        _builder.Services.AddTransient<MongoDbFactory>();
        _builder.Services.AddSingleton(serviceProvider => 
        {
            var factory = serviceProvider.GetService<MongoDbFactory>();
            var database = factory.CreateMongoDb();
            return database;
        });
    }

    /// <summary>Adds repository services.</summary>
    public void AddRepositories()
    {
        _builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
    }
}