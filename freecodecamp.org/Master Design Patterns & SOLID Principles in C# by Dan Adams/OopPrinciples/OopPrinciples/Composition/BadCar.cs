namespace OopPrinciples.Composition;

/**
 * Bad example: Car class contains all required logic.
 */
public class BadCar
{
    public void StartCar()
    {
        Console.WriteLine("Engine started");
        Console.WriteLine("Wheels rotating");
        Console.WriteLine("Car started");
    }
}