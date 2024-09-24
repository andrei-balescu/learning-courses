namespace DesignPatternsLibrary.OopPrinciples.Inheritance.BadExample;

public class BadBike
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public void Start()
    {
        Console.WriteLine("Bike is starting.");
    }

    public void Stop()
    {
        Console.WriteLine("Bike is stopping.");
    }
}