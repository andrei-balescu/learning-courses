using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.Prototype;

public class Rectangle : IShape
{
    private ILogger _logger;

    public int Width { get; set; } = 10;
    public int Height { get; set; } = 5;

    public Rectangle(ILogger logger)
    {
        _logger = logger;
    }

    public void Draw()
    {
        _logger.LogInformation($"Drawing a rectangle of {Width} x {Height}");
    }

    public IShape Duplicate()
    {
        var duplicate = new Rectangle(_logger)
        {
            Width = this.Width,
            Height = this.Height
        };
        return duplicate;
    }
}