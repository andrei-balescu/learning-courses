using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.AbstractFactory.UIMac;

public class MacUIComponentFactory : IUIComponentFactory
{
    private ILogger _logger;

    public MacUIComponentFactory(ILogger logger)
    {
        _logger = logger;
    }

    public IButton CreateButton()
    {
        var button = new MacButton(_logger);
        return button;
    }

    public ICheckbox CreateCheckbox()
    {
        var checkbox = new MacCheckbox(_logger);
        return checkbox;
    }
}