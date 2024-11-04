using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.AbstractFactory.UIMac;

public class MacButton : IButton
{
    private ILogger _logger;

    public MacButton(ILogger logger)
    {
        _logger = logger;
    }

    public void OnClick()
    {
        _logger.LogInformation("Mac: button has been clicked");
    }

    public void Render()
    {
        _logger.LogInformation("Mac: rendering button");
    }
}