using Playstore.Client.ServiceClients;
using Playstore.Client.Settings;
using Playstore.Common.DependencyInjection;

namespace Playstore.Client.DependencyInjection;

/// <summary>Dependency injection extensions.</summary>
public static class ServiceClients
{
    /// <summary>Adds clients to access different services.</summary>
    /// <param name="serviceCollection">Service collection to register clients to.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddServiceCients(this IServiceCollection services)
    {
        services.AddHttpClient<ICatalogClient, CatalogClient>((serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var clientSettings = configuration.GetSection(nameof(ServiceClientSettings)).Get<ServiceClientSettings>();

            client.BaseAddress = new Uri(clientSettings.CatalogServiceUrl);
        })
        .AddHttpClientResiliencePolicy<CatalogClient>(services);

        services.AddHttpClient<IInventoryClient, InventoryClient>((serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var clientSettings = configuration.GetSection(nameof(ServiceClientSettings)).Get<ServiceClientSettings>();

            client.BaseAddress = new Uri(clientSettings.InventoryServiceUrl);
        })
        .AddHttpClientResiliencePolicy<InventoryClient>(services);

        services.AddHttpClient<IAuthClient, AuthClient>((serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var clientSettings = configuration.GetSection(nameof(ServiceClientSettings)).Get<ServiceClientSettings>();

            client.BaseAddress = new Uri(clientSettings.AuthServiceUrl);
        })
        .AddHttpClientResiliencePolicy<AuthClient>(services);

        return services;
    }
}