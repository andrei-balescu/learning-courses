using Microsoft.Extensions.Logging;

namespace OopPrinciples.Composition;

public class Car
{
    private readonly ILogger _logger;

    private readonly Engine _engine;
    private readonly Wheels _wheels;

    public Car(ILogger logger)
    {
        _logger = logger;
        _engine = new Engine(_logger);
        _wheels = new Wheels(_logger);
    }

    public void StartCar()
    {
        _engine.Start();
        _wheels.Rotate();
        _logger.LogInformation("Car started");
    }
}