using SolidPrinciples.InterfaceSegregationPrinciple.BadExample;

namespace SolidPrinciples.InterfaceSegregationPrinciples;

/**
 * Bad example: even though this implementation of IBadShape is good the interface itself makes client classes prone to risks by not adhering the Open / Closed Principle.
 */
public class BadSphere : IBadShape
{
    public double Radius { get; set; }

    public double Area()
    {
        var area = 4 * Math.PI * Math.Pow(Radius, 2);
        return area;
    }

    public double Volume()
    {
        var volume = (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
        return volume;
    }
}