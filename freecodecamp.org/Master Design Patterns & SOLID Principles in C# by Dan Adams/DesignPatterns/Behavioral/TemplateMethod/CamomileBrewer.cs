using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.TemplateMethod;

public class CamomileBrewer : BeverageBrewer
{
    public CamomileBrewer(ILogger logger) : base(logger)
    {
    }

    protected override void Brew()
    {
        _logger.LogInformation("Brewing camomile for 3 minutes.");
    }
}