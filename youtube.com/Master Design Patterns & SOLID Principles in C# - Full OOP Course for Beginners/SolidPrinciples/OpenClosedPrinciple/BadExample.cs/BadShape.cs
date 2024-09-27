namespace SolidPrinciples.OpenClosedPrinciple.BadExample;

/**
 * Bad example: adding new shapes requires modifying shape class for each new shape.
 */
public class BadShape
{
    public BadShapeType Type { get; set; }
    public double Radius { get; set; }

    public double Length { get; set; }

    public double Width { get; set; }

    public double CalculateArea()
    {
        double area;
        switch(Type)
        {
            case BadShapeType.Circle:
                area = Math.PI * Math.Pow(Radius, 2);
                break;
            case BadShapeType.Rectangle:
                area = Length * Width;
                break;
            default:
                throw new InvalidOperationException("Shape type not supported");
        }

        return area;
    }
}