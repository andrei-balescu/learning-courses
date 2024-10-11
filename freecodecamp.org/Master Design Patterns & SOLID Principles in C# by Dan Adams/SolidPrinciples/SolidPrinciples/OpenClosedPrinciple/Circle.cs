namespace SolidPrinciples.OpenClosedPrinciple;

public class Circle : Shape
{
    public double Radius { get; set; }

    public override double CalculateArea()
    {
        var area = Math.PI * Math.Pow(Radius, 2);
        return area;
    }
}