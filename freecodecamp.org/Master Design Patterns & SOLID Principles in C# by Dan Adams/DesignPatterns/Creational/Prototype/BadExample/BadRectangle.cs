namespace DesignPatterns.Creational.Prototype.BadExample;

public class BadRectangle : IBadShape
{
    public int Width { get; set; } = 10;
    public int Height { get; set; } = 5;

    public void Draw()
    {
        Console.WriteLine($"Drawing a rectangle of {Width} x {Height}");
    }
}