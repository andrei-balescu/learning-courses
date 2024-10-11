using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Command.RemoteCommand;

public class Light
{
    private ILogger _logger;

    public Light(ILogger logger)
    {
        _logger = logger;
    }

    public void TurnOn()
    {
        _logger.LogInformation("Light is on.");
    }

    public void TurnOff()
    {
        _logger.LogInformation("Light is off.");
    }

    public void Dim()
    {
        _logger.LogInformation("Light is dim.");
    }
}