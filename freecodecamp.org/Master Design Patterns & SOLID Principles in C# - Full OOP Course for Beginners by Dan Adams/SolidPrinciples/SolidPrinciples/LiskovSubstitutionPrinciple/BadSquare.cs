namespace SolidPrinciples.LiskovSubstitutionPrinciple;

/**
 * Bad example: square's area calculetion will suppress either Width or Height. See unit tests.
 */
public class BadSquare : Rectangle
{
    public override double Width 
    { 
        get => base.Width; 
        set => base.Width = base.Height = value; 
    }

    public override double Height 
    { 
        get => base.Height; 
        set => base.Height = base.Width = value; 
    }
}