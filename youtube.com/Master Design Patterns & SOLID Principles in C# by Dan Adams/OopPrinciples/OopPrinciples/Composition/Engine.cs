using Microsoft.Extensions.Logging;

namespace OopPrinciples.Composition;

public class Engine
{
    private readonly ILogger _logger;

    public Engine(ILogger logger)
    {
        _logger = logger;
    }

    public void Start()
    {
        _logger.LogInformation("Engine started");
    }
}