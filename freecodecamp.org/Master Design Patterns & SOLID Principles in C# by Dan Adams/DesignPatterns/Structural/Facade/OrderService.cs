using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Facade;

public class OrderService
{
    private ILogger _logger;

    public OrderService(ILogger logger)
    {
        _logger = logger;
    }

    public void Order(OrderRequest orderRequest)
    {
        var authenticator = new SiteAuthenticator(_logger);
        authenticator.Authenticate(orderRequest.Name);

        var inventory = new SiteInventory(_logger);
        foreach(string itemId in orderRequest.ItemIds)
        {
            inventory.CheckInventory(itemId);
        }

        var payment = new SitePayment(_logger, orderRequest.Name, orderRequest.CardNumber, orderRequest.Amount);
        payment.Pay();

        var orderFulfillment = new SiteOrderFulfillment(_logger, inventory);
        orderFulfillment.Fulfill(orderRequest.Name, orderRequest.Address, orderRequest.ItemIds);
    }
}