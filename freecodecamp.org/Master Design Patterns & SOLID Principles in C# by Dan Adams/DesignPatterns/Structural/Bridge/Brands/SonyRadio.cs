using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Bridge.Brands;

public class SonyRadio : IRemoteControlledDeviceDevice
{
    private ILogger _logger;

    public SonyRadio(ILogger logger)
    {
        _logger = logger;
    }

    public void TurnOff()
    {
        _logger.LogInformation("Turning Sony radio off");
    }

    public void TurnOn()
    {
        _logger.LogInformation("Turning Sony radio on");
    }

    public void SetChannel(int channel)
    {
        _logger.LogInformation($"Setting Sony channel to {channel}");
    }
}