using Microsoft.Extensions.Logging;

namespace OopPrinciples.Polymorphism;

public abstract class Vehicle
{
    protected readonly ILogger<Vehicle> _logger;

    public Vehicle(ILogger<Vehicle> logger)
    {
        _logger = logger;
    }

    public abstract void Start();

    public abstract void Stop();
}