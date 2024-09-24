using Microsoft.Extensions.Logging;

namespace DesignPatternsLibrary.OopPrinciples.Inheritance;

public abstract class Vehicle
{
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }

    private readonly ILogger _logger;

    public Vehicle(string brand, string model, int year, ILogger logger)
    {
        Brand = brand;
        Model = model;
        Year = year;

        _logger = logger;
    }

    public void Start()
    {
        _logger.LogInformation("Vehicle is starting.");
    }

    public void Stop()
    {
        _logger.LogInformation("Vehicle is stopping.");
    }
}