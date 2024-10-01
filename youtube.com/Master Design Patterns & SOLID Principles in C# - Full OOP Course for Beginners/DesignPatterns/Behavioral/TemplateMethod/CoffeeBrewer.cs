using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.TemplateMethod;

public class CoffeeBrewer : BeverageBrewer
{
    public CoffeeBrewer(ILogger logger) : base(logger)
    {
    }

    protected override void Brew()
    {
        _logger.LogInformation("Brewing coffee for 5 minutes.");   
    }

    protected override void AddCondiments()
    {
        _logger.LogInformation("Adding cream to the coffee.");
    }
}