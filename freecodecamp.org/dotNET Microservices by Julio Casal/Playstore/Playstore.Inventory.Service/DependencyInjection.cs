using Playstore.Inventory.Service.Clients;
using Playstore.Inventory.Service.Settings;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;

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
        })
        .AddPolicyHandler((serviceProvider, httpRequestMessage) => 
        {
            PolicyBuilder<HttpResponseMessage> policyBuilder = HttpPolicyExtensions.HandleTransientHttpError()
                .Or<TimeoutRejectedException>();
            AsyncRetryPolicy<HttpResponseMessage> asyncRetryPolicy = policyBuilder.WaitAndRetryAsync(
                    5, 
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (outcome, timeSpan, retryAttempt, context) => 
                    {
                        ILogger logger = serviceProvider.GetService<ILogger<CatalogClient>>();
                        logger.LogWarning($"Delaying for {timeSpan.TotalSeconds} seconds, then making retry: attempt {retryAttempt}");
                    });
            return asyncRetryPolicy;
        })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(1)));

        return serviceCollection;
    }
}