namespace DesignPatterns.Structural.Bridge;

public class AdvancedRemoteControl : RemoteControl
{
    public AdvancedRemoteControl(IRemoteControlledDeviceDevice device) : base(device)
    {
    }

    public void SetChannel(int channel)
    {
        _device.SetChannel(channel);
    }
}