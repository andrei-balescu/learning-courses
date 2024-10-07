using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Facade;

public class SiteInventory
{
    private ILogger _logger;

    public SiteInventory(ILogger logger)
    {
        _logger = logger;
    }

    public void CheckInventory(string itemId)
    {
        _logger.LogInformation($"Checking inventory for item {itemId}");
    }

    public void ReduceInventory(string itemId, int amount)
    {
        _logger.LogInformation($"Reducing inventory of {itemId} by {amount}");
    }
}