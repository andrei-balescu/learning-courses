using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using Polly.Timeout;

namespace Playstore.Common.DependencyInjection;

public static class HttpClientResiliencePolicies
{
    /// <summary>Add HTTP rquest resilience to <see cref="IHttpClientBuilder"/>. </summary>
    /// <typeparam name="TClient">The client using the policies.</typeparam>
    /// <param name="httpClientBuilder">Adds policies to this instance.</param>
    /// <param name="serviceCollection">Service collections to register policies to.</param>
    /// <returns>An instance of <see cref="IHttpClientBuilder"/></returns>
    public static IHttpClientBuilder AddHttpClientResiliencePolicy<TClient>(this IHttpClientBuilder httpClientBuilder, IServiceCollection serviceCollection)
        where TClient : class
    {
        // create policy registry to save policies for singleton reuse
        IPolicyRegistry<string> policyRegistry = serviceCollection.AddPolicyRegistry();

        httpClientBuilder.AddPolicyHandler(
            (serviceProvider, httpRequestMessage, keySelector) => CreateResiliencePolicies<TClient>(serviceProvider), 
            // Save policy in registry to be used as singleton with the provided key
            // NOTE: Cirquit breaker policy REQUIRES singleton usage
            httpRequestMessage => typeof(TClient).FullName);

        return httpClientBuilder;
    }

    /// <summary>Creates resilience policies: timeout, retry, circuit breaker.</summary>
    /// <typeparam name="TClient">The client using the policies.</typeparam>
    /// <param name="serviceProvider">Provider for accessing app services.</param>
    /// <returns>Combined resilience policies.</returns>
    private static IAsyncPolicy<HttpResponseMessage> CreateResiliencePolicies<TClient>(IServiceProvider serviceProvider)
        where TClient:class
    {
            // BUG(?!): policies get re-created on each request causing circuit breaker state to be ignored
            IAsyncPolicy<HttpResponseMessage> timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(2));

            PolicyBuilder<HttpResponseMessage> policyBuilder = HttpPolicyExtensions.HandleTransientHttpError()
                .Or<TimeoutRejectedException>();

            IAsyncPolicy<HttpResponseMessage> circuitBreakerPolicy = policyBuilder.CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 3,
                durationOfBreak: TimeSpan.FromSeconds(60),
                onBreak: (outcome, timeSpan) =>
                {
                    ILogger logger = serviceProvider.GetService<ILogger<TClient>>();
                    logger.LogWarning($"Opening request circuit for {timeSpan.TotalSeconds} seconds...");
                },
                onReset: () =>
                {
                    ILogger logger = serviceProvider.GetService<ILogger<TClient>>();
                    logger.LogWarning("Closing the request circuit...");
                }
            );
            
            IAsyncPolicy<HttpResponseMessage> retryPolicy = policyBuilder.WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timeSpan, retryAttempt, context) =>
                {
                    ILogger logger = serviceProvider.GetService<ILogger<TClient>>();
                    logger.LogWarning($"Delaying for {timeSpan.TotalSeconds} seconds, then making retry: attempt {retryAttempt}");
                });

            IAsyncPolicy<HttpResponseMessage> policies = circuitBreakerPolicy.WrapAsync(timeoutPolicy);
            policies = retryPolicy.WrapAsync(policies);

            return policies;
    }
}