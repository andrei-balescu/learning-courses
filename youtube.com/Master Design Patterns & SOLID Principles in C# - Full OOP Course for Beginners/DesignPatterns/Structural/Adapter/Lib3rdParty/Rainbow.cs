using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Adapter.Lib3rdParty;

public class Rainbow
{
    private float _brightness;

    private ILogger _logger;

    public Rainbow(ILogger logger)
    {
        _logger = logger;
    }

    public void Setup(float brightness)
    {
        _brightness = brightness;
        _logger.LogInformation($"Setting up filter with brightness: {_brightness}");
    }

    public void Update(Video video)
    {
        _logger.LogInformation("Applying rainbow color to video");
    }
}