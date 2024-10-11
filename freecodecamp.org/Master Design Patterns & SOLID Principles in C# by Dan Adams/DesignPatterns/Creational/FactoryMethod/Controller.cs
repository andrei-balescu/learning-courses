using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.FactoryMethod;

public class Controller
{
    private ILogger _logger;

    public Controller(ILogger logger)
    {
        _logger = logger;
    }

    public void Render(string fileName, IDictionary<string, object> data)
    {
        var viewEngine = CreateViewEngine();
        var html = viewEngine.Render(fileName, data);

        _logger.LogInformation(html);
    }

    protected virtual IViewEngine CreateViewEngine()
    {
        var viewEngine = new BladeViewEngine();
        return viewEngine;
    }
}