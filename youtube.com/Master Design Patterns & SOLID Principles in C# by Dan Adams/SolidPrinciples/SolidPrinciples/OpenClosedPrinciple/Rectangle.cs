namespace SolidPrinciples.OpenClosedPrinciple;

public class Rectangle : Shape
{
    public double Length { get; set; }

    public double Width { get; set; }

    public override double CalculateArea()
    {
        var area = Length * Width;
        return area;
    }
}