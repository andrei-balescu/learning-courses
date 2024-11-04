using DesignPatterns.Creational.AbstractFactory.UIMac;
using DesignPatterns.Creational.AbstractFactory.UIWindows;
using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.AbstractFactory;

public class BadUserSettingsForm
{
    private ILogger _logger;

    public BadUserSettingsForm(ILogger logger)
    {
        _logger = logger;
    }

    public void Render(BadOperatingSystemType operatingSystemType)
    {
        switch(operatingSystemType)
        {
            case BadOperatingSystemType.Windows:
                var windowsButton = new WindowsButton(_logger);
                windowsButton.Render();
                var windowsCheckbox = new WindowsCheckbox(_logger);
                windowsCheckbox.Render();
                break;
            case BadOperatingSystemType.Mac:
                var macButton = new MacButton(_logger);
                macButton.Render();
                var macCheckbox = new MacCheckbox(_logger);
                macCheckbox.Render();
                break;
            default:
                throw new Exception("Unsupported operating system");
        }
    }
}