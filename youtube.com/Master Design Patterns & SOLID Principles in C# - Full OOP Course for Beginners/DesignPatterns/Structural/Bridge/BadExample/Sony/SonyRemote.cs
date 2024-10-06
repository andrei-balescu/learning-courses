namespace DesignPatterns.Structural.Bridge.BadExample.Sony;

public class SonyRemote : RemoteControl
{
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