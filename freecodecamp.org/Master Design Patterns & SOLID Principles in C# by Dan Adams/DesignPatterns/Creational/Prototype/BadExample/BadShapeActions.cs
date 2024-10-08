namespace DesignPatterns.Creational.Prototype.BadExample;

public class BadShapeActions
{
    public void Duplicate(IBadShape shape)
    {
        if (shape is BadCircle)
        {
            var circle = (BadCircle)shape;
            var newCircle = new BadCircle { Radius = circle.Radius };
            newCircle.Draw();   
        }
        if (shape is BadRectangle)
        {
            var rectangle = (BadRectangle)shape;
            var newRectangle = new BadRectangle
            {
                Width = rectangle.Width,
                Height = rectangle.Height
            };
            newRectangle.Draw();
        }
        else
        {
            throw new ArgumentException("Invalid shape provided");
        }
    }
}