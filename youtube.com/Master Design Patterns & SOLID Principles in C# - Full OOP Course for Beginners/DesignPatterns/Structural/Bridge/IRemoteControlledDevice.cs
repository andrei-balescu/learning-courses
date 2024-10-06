namespace DesignPatterns.Structural.Bridge;

public interface IRemoteControlledDeviceDevice
{
    void TurnOff();
    void TurnOn();
    void SetChannel(int channel);
}