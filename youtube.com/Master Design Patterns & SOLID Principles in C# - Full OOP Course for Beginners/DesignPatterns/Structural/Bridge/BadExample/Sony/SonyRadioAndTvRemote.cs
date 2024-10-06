namespace DesignPatterns.Structural.Bridge.BadExample.Sony;

public class SonyRadioAndTvRemote : RadioAndTvRemote
{
    public override void ControlRadio()
    {
        Console.WriteLine("Now controlling Sony radio");
    }

    public override void ControlTv()
    {
        Console.WriteLine("Now controlling Sony TV");
    }

    public override void TurnOff()
    {
        Console.WriteLine("Turning Sony radio off");
    }

    public override void TurnOn()
    {
        Console.WriteLine("Turning Sony radio on");
    }

    public override void VolumeDown()
    {
        Console.WriteLine("Turning Sony radio volume down");
    }

    public override void VolumeUp()
    {
        Console.WriteLine("Turning Sony radio volume up");
    }
}