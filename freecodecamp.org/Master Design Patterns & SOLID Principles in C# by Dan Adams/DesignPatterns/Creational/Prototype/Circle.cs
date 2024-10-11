using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.Prototype;

public class Circle : IShape
{
    private ILogger _logger;

    public int Radius { get; set; } = 5;

    public Circle(ILogger logger)
    {
        _logger = logger;
    }

    public void Draw()
    {
        _logger.LogInformation($"Drawing a  circle with the radius of {Radius}");
    }

    public IShape Duplicate()
    {
        var duplicate = new Circle(_logger) { Radius = this.Radius };
        return duplicate;
    }
}