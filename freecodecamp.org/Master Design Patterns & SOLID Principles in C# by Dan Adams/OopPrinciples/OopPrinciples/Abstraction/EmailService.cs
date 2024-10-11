using Microsoft.Extensions.Logging;

namespace OopPrinciples.Abstraction;

public class EmailService
{
    private readonly ILogger _logger;

    /**
     * Capture messages for unit testing.
     */
    public const string MSG_SENDING = "Sending email: ";
    public const string MSC_CONNECTING = "Connecting to email server...";
    public const string MSG_AUTHENTICATING = "Authenticating user...";
    public const string MSG_DISCONNECTING = "Disconnecting from email server...";

    public EmailService(ILogger logger)
    {
        _logger = logger;
    }

    public void SendEmail()
    {
        Connect();
        Authenticate();
        _logger.LogInformation(MSG_SENDING);
        Disconnect();
    }

    private void Connect()
    {
        _logger.LogInformation(MSC_CONNECTING);
    }

    private void Authenticate()
    {
        _logger.LogInformation(MSG_AUTHENTICATING);
    }

    private void Disconnect()
    {
        _logger.LogInformation(MSG_DISCONNECTING);
    }
}