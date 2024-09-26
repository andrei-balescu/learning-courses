using Microsoft.Extensions.Logging;

namespace OopPrinciples.Polymorphism;

public abstract class Vehicle
{
    protected readonly ILogger<Vehicle> _logger;

    public Vehicle(ILogger<Vehicle> logger)
    {
        _logger = logger;
    }

    public virtual void Start()
    {
        _logger.LogInformation("Vehicle is starting.");
    }

    public virtual void Stop()
    {
        _logger.LogInformation("Vehicle is stopping.");
    }
}