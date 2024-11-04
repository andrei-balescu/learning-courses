namespace DesignPatterns.Creational.Prototype;

public interface IShape
{
    void Draw();
    IShape Duplicate();
}