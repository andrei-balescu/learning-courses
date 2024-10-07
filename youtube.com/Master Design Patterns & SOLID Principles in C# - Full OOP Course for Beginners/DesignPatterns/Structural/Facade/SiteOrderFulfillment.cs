using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Facade;

public class SiteOrderFulfillment
{
    private ILogger _logger;

    private SiteInventory _siteInventory;

    public SiteOrderFulfillment(ILogger logger, SiteInventory siteInventory)
    {
        _siteInventory = siteInventory;
        _logger = logger;
    }

    public void Fulfill(string name, string address, IEnumerable<string> items)
    {
        _logger.LogInformation("Inserting items into database");
        foreach(string item in items)
        {
            _siteInventory.ReduceInventory(item, 1);
        }
    }
}