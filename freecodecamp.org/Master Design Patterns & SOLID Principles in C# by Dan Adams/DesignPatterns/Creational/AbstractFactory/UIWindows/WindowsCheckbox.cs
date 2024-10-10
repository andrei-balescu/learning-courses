using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.AbstractFactory.UIWindows;

public class WindowsCheckbox : ICheckbox
{
    private ILogger _logger;

    public WindowsCheckbox(ILogger logger)
    {
        _logger = logger;
    }

    public void OnSelect()
    {
        _logger.LogInformation("Windows: checkbox has been selected");
    }

    public void Render()
    {
        _logger.LogInformation("Windows: rendering checkbox");
    }
}