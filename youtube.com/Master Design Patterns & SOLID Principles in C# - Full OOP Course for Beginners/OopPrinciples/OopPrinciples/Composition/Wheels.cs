using Microsoft.Extensions.Logging;

namespace OopPrinciples.Composition;

public class Wheels
{
    private readonly ILogger _logger;

    public Wheels(ILogger logger)
    {
        _logger = logger;
    }

    public void Rotate()
    {
        _logger.LogInformation("Wheels rotating");
    }
}