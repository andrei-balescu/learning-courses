using Microsoft.Extensions.Logging;

namespace SolidPrinciples.DependencyInversionPrinciple;

public class Car
{
    private IEngine _engine;
    private ILogger _logger;

    public Car(IEngine engine, ILogger logger)
    {
        _engine = engine;
        _logger = logger;
    }

    public void StartCar()
    {
        _engine.Start();
        _logger.LogInformation("Car started.");
    }
}