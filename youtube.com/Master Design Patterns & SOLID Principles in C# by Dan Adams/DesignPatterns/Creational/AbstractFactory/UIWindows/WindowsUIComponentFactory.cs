using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.AbstractFactory.UIWindows;

public class WindowsUIComponentFactory : IUIComponentFactory
{
    private ILogger _logger;

    public WindowsUIComponentFactory(ILogger logger)
    {
        _logger = logger;
    }

    public IButton CreateButton()
    {
        var button = new WindowsButton(_logger);
        return button;
    }

    public ICheckbox CreateCheckbox()
    {
        var checkbox = new WindowsCheckbox(_logger);
        return checkbox;
    }
}