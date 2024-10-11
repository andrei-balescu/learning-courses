namespace DesignPatterns.Structural.Bridge;

public class RemoteControl
{
    protected IRemoteControlledDeviceDevice _device;

    public RemoteControl(IRemoteControlledDeviceDevice device)
    {
        _device = device;
    }

    public void TurnOn()
    {
        _device.TurnOn();
    }

    public void TurnOff()
    {
        _device.TurnOff();
    }
}