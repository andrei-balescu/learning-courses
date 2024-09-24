using DesignPatternsLibrary.OopPrinciples.Polymorphism;
using Microsoft.Extensions.Logging;

namespace DesignPatternsLibrary.OopPrinciples.Polymorphism;

public class Car : Vehicle
{
    public Car (ILogger<Car> logger) : base(logger)
    {

    }

    public override void Start()
    {
        _logger.LogInformation("Car is starting.");
    }

    public override void Stop()
    {
        _logger.LogInformation("Car is stopping.");
    }
}