using Microsoft.Extensions.Logging;

namespace SolidPrinciples.SingleResponsabilityPrinciple;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(ILogger logger)
    {
        _logger = logger;
    }

    public void SendEmail(string email, string message)
    {
        _logger.LogInformation($"Sending email to {email}: {message}");
    }
}