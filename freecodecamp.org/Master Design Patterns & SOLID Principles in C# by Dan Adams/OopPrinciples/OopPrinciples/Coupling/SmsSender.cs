using Microsoft.Extensions.Logging;

namespace OopPrinciples.Coupling;

public class SmsSender : INotificationService
{
    private readonly ILogger _logger;

    public SmsSender(ILogger logger)
    {
        _logger = logger;
    }

    public void SendNotification(string message)
    {
        _logger.LogInformation("Sending SMS: " + message);
    }
}