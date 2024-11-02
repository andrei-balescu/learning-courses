using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playstore.Common.Settings;

namespace Playstore.Common.DependencyInjection;

public static class RabbitMqRegistation
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMassTransit(busRegistrationConfigurator =>
        {
            busRegistrationConfigurator.AddConsumers(Assembly.GetEntryAssembly());

            busRegistrationConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var configuration = context.GetService<IConfiguration>();
                var rabbitMqSettings = configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
                configurator.Host(rabbitMqSettings.Host);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(rabbitMqSettings.EndpointPrefix, false));
            });
        });

        return serviceCollection;
    }
}