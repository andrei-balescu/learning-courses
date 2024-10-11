
namespace DesignPatterns.Creational.FactoryMethod;

public class BladeViewEngine : IViewEngine
{
    public string Render(string fileName, IDictionary<string, object> data)
    {
        var view = $"View rendered from {fileName} by Blade";
        return view;
    }
}