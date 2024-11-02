using Playstore.Common.DependencyInjection;
using Playstore.Inventory.Service.Clients;
using Playstore.Inventory.Service.Settings;

namespace Playstore.Inventory.Service;

/// <summary>Dependency injection extensions.</summary>
public static class DependencyInjection
{
    /// <summary>Adds clients to access different services.</summary>
    /// <param name="serviceCollection">Service collection to register clients to.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddServiceClients(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<CatalogClient>((serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var clientSettings = configuration.GetSection(nameof(ServiceClientSettings)).Get<ServiceClientSettings>();

            client.BaseAddress = new Uri(clientSettings.CatalogServiceUrl);
        }).AddHttpClientResiliencePolicy<CatalogClient>(serviceCollection);
        return serviceCollection;
    }
}
