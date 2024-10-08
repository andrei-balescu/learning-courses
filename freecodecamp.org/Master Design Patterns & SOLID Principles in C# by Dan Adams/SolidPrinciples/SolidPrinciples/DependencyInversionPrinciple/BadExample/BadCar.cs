using SolidPrinciples.DependencyInversionPrinciple.BadExample;

namespace SolidPrinciples.DependencyInversionPrinciple;

public class BadCar
{
    private BadEngine _engine;

    public BadCar()
    {
        _engine = new BadEngine();
    }

    public void StartCar()
    {
        _engine.Start();
        Console.WriteLine("Car started.");
    }
}