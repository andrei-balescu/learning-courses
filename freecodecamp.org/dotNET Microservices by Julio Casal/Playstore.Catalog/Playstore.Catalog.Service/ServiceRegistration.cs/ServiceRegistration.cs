using Playstore.Catalog.Service.Repositories;
using Playstore.Catalog.Service.Settings;

namespace Playstore.Catalog.Service.ServiceRegistration;

public class ServiceRegistration
{
    private WebApplicationBuilder _builder;

    public ServiceRegistration(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    public void AddMongoDb()
    {
        _builder.Services.Configure<MongoDbSettings>(_builder.Configuration.GetSection(nameof(MongoDbSettings)));
        _builder.Services.AddTransient<MongoDbFactory>();
        _builder.Services.AddSingleton(serviceProvider => 
        {
            var factory = serviceProvider.GetService<MongoDbFactory>();
            var database = factory.CreateMongoDb();
            return database;
        });
    }

    public void AddRepositories()
    {
        _builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
    }
}