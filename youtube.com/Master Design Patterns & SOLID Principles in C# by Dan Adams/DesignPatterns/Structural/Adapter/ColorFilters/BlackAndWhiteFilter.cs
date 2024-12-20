using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Adapter.ColorFilters;

public class BlackAndWhiteFilter : IColorFilter
{
    public ILogger _logger;

    public BlackAndWhiteFilter(ILogger logger)
    {
        _logger = logger;
    }

    public void Apply(Video video)
    {
        _logger.LogInformation("Applying black and white filter to video");
    }
}