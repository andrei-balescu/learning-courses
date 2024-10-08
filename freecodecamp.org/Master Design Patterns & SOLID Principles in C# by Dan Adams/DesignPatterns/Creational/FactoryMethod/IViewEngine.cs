namespace DesignPatterns.Creational.FactoryMethod;

public interface IViewEngine
{
    string Render(string fileName, IDictionary<string, object> data);
}