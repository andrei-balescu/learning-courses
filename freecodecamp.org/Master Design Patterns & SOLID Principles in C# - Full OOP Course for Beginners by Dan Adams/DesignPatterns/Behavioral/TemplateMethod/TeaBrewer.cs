using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.TemplateMethod;

public class TeaBrewer : BeverageBrewer
{
    public TeaBrewer(ILogger logger) : base(logger)
    {
    }

    protected override void Brew()
    {
        _logger.LogInformation("Brewing tea for 3 minutes.");
    }

    protected override void AddCondiments()
    {
        _logger.LogInformation("Adding lemon to the tea.");
    }
}