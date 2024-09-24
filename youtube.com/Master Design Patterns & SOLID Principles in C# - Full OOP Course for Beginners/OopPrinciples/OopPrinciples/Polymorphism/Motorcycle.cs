using Microsoft.Extensions.Logging;

namespace DesignPatternsLibrary.OopPrinciples.Polymorphism;

public class Motorcycle : Vehicle
{
    public Motorcycle(ILogger<Motorcycle> logger) : base(logger)
    {

    }

    public override void Start()
    {
        _logger.LogInformation("Motorcycle is starting.");
    }

    public override void Stop()
    {
        _logger.LogInformation("Motorcycle is stopping.");
    }
}