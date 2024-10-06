using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Bridge.Brands;

public class SamsungRadio : IRemoteControlledDeviceDevice
{
    private ILogger _logger;

    public SamsungRadio(ILogger logger)
    {
        _logger = logger;
    }

    public void TurnOff()
    {
        _logger.LogInformation("Turning Samsung radio off");
    }

    public void TurnOn()
    {
        _logger.LogInformation("Turning Samsung radio on");
    }

    public void SetChannel(int channel)
    {
        _logger.LogInformation($"Setting Samsung channel to {channel}");
    }
}