using Microsoft.Extensions.Logging;

namespace SolidPrinciples.DependencyInversionPrinciple;

public class Engine : IEngine
{
    private ILogger _logger;

    public Engine(ILogger logger)
    {
        _logger = logger;
    }

    public void Start()
    {
        _logger.LogInformation("Engine started.");
    }
}