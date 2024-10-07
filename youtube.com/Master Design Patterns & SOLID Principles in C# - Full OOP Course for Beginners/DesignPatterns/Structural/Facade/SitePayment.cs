using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Facade;

public class SitePayment
{
    private ILogger _logger;

    private readonly string _name;
    private readonly string _cardNumber;
    private readonly decimal _amount;

    public SitePayment(ILogger logger, string name, string cardNumber, decimal amount)
    {
        _name = name;
        _cardNumber = cardNumber;
        _amount = amount;
        _logger = logger;
    }

    public void Pay()
    {
        _logger.LogInformation($"Charging card {_cardNumber} for {_amount}");
    }
}