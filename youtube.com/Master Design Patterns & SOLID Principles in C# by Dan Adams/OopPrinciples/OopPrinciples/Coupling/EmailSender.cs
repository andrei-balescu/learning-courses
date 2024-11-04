using Microsoft.Extensions.Logging;

namespace OopPrinciples.Coupling;

public class EmailSender : INotificationService
{
    private readonly ILogger _logger;

    public EmailSender(ILogger logger)
    {
        _logger = logger;
    }

    public void SendNotification(string message)
    {
        _logger.LogInformation("Sending email: " + message);
    }
}