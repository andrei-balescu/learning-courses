using Microsoft.Extensions.Logging;

namespace DesignPatternsLibrary.OopPrinciples.Inheritance;

public class Bike : Vehicle
{
    public Bike (string brand, string model, int year, ILogger logger) : base(brand, model, year, logger)
    {
        
    }
}