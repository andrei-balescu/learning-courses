using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Adapter.VideoFilters;

public class MidnightPurpleFilter : IVideoFilter
{
    private ILogger _logger;

    public MidnightPurpleFilter(ILogger logger)
    {
        _logger = logger;
    }

    public void Apply(Video video)
    {
        _logger.LogInformation("Applying midnight-purple filter to video");
    }
}