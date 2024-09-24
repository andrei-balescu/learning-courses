using Microsoft.Extensions.Logging;

namespace DesignPatternsLibrary.OopPrinciples.Inheritance;

public class Car : Vehicle
{
    public int NumberOfDoors { get; private set; }

    public Car(string brand, string model, int year, int numberOfDoors, ILogger logger) : base(brand, model, year, logger)
    {
        NumberOfDoors = numberOfDoors;
    }
}