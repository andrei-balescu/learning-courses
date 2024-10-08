namespace DesignPatterns.Structural.Bridge.BadExample;

public abstract class BadRadioAndTvRemote : BadRemoteControl
{
    public abstract void ControlTv();
    public abstract void ControlRadio();
}