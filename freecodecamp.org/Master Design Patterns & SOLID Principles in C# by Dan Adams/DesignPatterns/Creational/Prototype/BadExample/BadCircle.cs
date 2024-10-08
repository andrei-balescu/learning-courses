namespace DesignPatterns.Creational.Prototype.BadExample;

public class BadCircle : IBadShape
{
    public int Radius { get; set; } = 5;

    public void Draw()
    {
        Console.WriteLine($"Drawing a  circle with the radius of {Radius}");
    }
}