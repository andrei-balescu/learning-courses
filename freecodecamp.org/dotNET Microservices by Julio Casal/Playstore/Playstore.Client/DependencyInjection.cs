using Playstore.Client.ServiceClients;
using Playstore.Client.Settings;
using Playstore.Common.DependencyInjection;

namespace Playstore.Client;

/// <summary>Dependency injection extensions.</summary>
public static class DependencyInjection
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

        return services;
    }
}