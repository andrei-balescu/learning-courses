namespace SolidPrinciples.InterfaceSegregationPrinciple;

public class Circle : IShape2D
{
    public double Radius { get; set; }

    public double Area()
    {
        var area = Math.PI * Math.Pow(Radius, 2);
        return area;
    }
}