using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Strategy.Overlay;

public class OverlayBlur : IOverlay
{
    private ILogger _logger;

    public OverlayBlur(ILogger logger)
    {
        _logger = logger;
    }

    public void Apply()
    {
        _logger.LogInformation("Applying blur overlay.");
    }
}