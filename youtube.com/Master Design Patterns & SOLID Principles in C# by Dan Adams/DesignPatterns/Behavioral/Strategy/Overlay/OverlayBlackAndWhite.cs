using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Strategy.Overlay;

public class OverlayBlackAndWhite : IOverlay
{
    private ILogger _logger;

    public OverlayBlackAndWhite(ILogger logger)
    {
        _logger = logger;
    }

    public void Apply()
    {
        _logger.LogInformation("Applying black and white overlay.");
    }
}