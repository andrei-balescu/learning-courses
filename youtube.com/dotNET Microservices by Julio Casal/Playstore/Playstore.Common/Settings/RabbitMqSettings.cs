namespace Playstore.Common.Settings;

/// <summary>Represents the RabbitMQ settings in <c>appsettings.json</c>.</summary>
public class RabbitMqSettings
{
    /// <summary>RabbitMQ host.</summary>
    public string Host { get; init; }

    /// <summary>RabbitMQ endpoint prefix</summary>
    public string EndpointPrefix { get; init; }
}