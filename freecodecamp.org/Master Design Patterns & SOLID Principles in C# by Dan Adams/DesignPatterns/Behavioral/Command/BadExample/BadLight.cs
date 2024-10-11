namespace DesignPatterns.Behavioral.Command.BadExample;

public class BadLight
{
    public void TurnOn()
    {
        Console.WriteLine("Light is on");
    }

    public void TurnOff()
    {
        Console.WriteLine("Light is off");
    }
}