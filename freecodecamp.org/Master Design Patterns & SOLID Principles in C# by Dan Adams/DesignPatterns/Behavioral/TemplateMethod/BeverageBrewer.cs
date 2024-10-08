using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.TemplateMethod;

public abstract class BeverageBrewer : IBeverageBrewer
{
    protected ILogger _logger;

    public BeverageBrewer(ILogger logger) 
    {
        _logger = logger;
    }

    public void Prepare(bool addCondiments = false)
    {
        BoilWater();
        PourWaterIntoCup();
        Brew();
        if (addCondiments)
        { 
            AddCondiments();
        }
    }

    private void BoilWater()
    {
        _logger.LogInformation("Boiling water.");
    }

    private void PourWaterIntoCup()
    {
        _logger.LogInformation("Pouring water into cup.");
    }

    protected abstract void Brew();

    protected virtual void AddCondiments()
    {
        // do nothing by default.
    }
}