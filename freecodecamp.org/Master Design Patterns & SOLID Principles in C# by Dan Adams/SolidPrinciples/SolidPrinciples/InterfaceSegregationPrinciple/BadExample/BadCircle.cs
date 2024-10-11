namespace SolidPrinciples.InterfaceSegregationPrinciple.BadExample;

public class BadCircle : IBadShape
{
    public double Radius { get; set; }

    public double Area()
    {
        var area = Math.PI * Math.Pow(Radius, 2);
        return area;
    }

    public double Volume()
    {
        throw new InvalidOperationException("Volume not applicable for 2D shapes.");
    }
}