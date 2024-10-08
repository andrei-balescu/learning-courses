namespace DesignPatterns.Structural.Bridge.BadExample.Sony;

public class BadAdvancedSonyRemote : BadAdvancedRemoteControl
{
    public override void SetChannel(int channel)
    {
        Console.WriteLine($"Setting Sony channel to {channel}");
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