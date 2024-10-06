namespace DesignPatterns.Structural.Bridge.BadExample;

public abstract class RadioAndTvRemote : RemoteControl
{
    public abstract void ControlTv();
    public abstract void ControlRadio();
}