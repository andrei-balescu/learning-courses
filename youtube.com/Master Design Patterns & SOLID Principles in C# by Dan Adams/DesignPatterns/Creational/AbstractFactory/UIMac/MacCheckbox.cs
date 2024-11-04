using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.AbstractFactory;

public class MacCheckbox : ICheckbox
{
    private ILogger _logger;

    public MacCheckbox(ILogger logger)
    {
        _logger = logger;
    }

    public void OnSelect()
    {
        _logger.LogInformation("Mac: checkbox has been selected");
    }

    public void Render()
    {
        _logger.LogInformation("Mac: rendering checkbox");
    }
}