using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.AbstractFactory.UIWindows;

public class WindowsButton : IButton
{
    private ILogger _logger;

    public WindowsButton(ILogger logger)
    {
        _logger = logger;
    }

    public void OnClick()
    {
        _logger.LogInformation("Windows: button has been clicked");
    }

    public void Render()
    {
        _logger.LogInformation("Windows: rendering button");
    }
}