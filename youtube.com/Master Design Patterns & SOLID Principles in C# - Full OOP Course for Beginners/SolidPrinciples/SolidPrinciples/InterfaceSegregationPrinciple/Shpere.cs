namespace SolidPrinciples.InterfaceSegregationPrinciple;

public class Sphere : IShape3D
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