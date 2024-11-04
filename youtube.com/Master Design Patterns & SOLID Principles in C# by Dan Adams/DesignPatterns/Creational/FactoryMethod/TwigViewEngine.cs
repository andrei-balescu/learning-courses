
namespace DesignPatterns.Creational.FactoryMethod;

public class TwigViewEngine : IViewEngine
{
    public string Render(string fileName, IDictionary<string, object> data)
    {
        var view = $"View rendered from {fileName} by Twig";
        return view;
    }
}