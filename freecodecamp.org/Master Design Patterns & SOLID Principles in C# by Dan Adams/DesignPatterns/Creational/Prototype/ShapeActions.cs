namespace DesignPatterns.Creational.Prototype;

public class ShapeActions
{
    public IShape Duplicate(IShape shape)
    {
        IShape duplicate = shape.Duplicate();
        return duplicate;
    }
}