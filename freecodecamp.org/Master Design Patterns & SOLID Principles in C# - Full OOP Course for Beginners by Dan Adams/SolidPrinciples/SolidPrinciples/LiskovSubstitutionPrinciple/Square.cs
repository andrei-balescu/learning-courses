namespace SolidPrinciples.LiskovSubstitutionPrinciple;

public class Square : Shape
{
    public double SideLength { get; set; }

    public override double Area => Math.Pow(SideLength, 2);
}