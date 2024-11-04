namespace DesignPatterns.Structural.Bridge.BadExample.Samsung;

public class BadSamsungRemote : BadRemoteControl
{
    public override void TurnOff()
    {
        Console.WriteLine("Turning Samsung radio off");
    }

    public override void TurnOn()
    {
        Console.WriteLine("Turning Samsung radio on");
    }

    public override void VolumeDown()
    {
        Console.WriteLine("Turning Samsung radio volume down");
    }

    public override void VolumeUp()
    {
        Console.WriteLine("Turning Samsung radio volume up");
    }
}