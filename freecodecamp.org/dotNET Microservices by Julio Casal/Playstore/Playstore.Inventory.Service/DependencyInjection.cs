using Playstore.Inventory.Service.Clients;
using Playstore.Inventory.Service.Settings;

namespace Playstore.Inventory.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceClients(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<CatalogClient>((serviceProvider, client) => 
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var clientSettings = configuration.GetSection(nameof(ServiceClientSettings)).Get<ServiceClientSettings>();

            client.BaseAddress = new Uri(clientSettings.CatalogServiceUrl);
        });

        return serviceCollection;
    }
}