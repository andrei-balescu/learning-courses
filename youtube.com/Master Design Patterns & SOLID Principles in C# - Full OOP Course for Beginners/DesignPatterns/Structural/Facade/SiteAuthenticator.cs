using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Facade;

public class SiteAuthenticator
{
    private ILogger _logger;

    public SiteAuthenticator(ILogger logger)
    {
        _logger = logger;
    }

    public void Authenticate(string userName)
    {
        _logger.LogInformation($"Authenticating {userName}");
    }
}