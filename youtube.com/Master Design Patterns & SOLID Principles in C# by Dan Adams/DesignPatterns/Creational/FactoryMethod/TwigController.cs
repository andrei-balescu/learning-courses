using Microsoft.Extensions.Logging;

namespace DesignPatterns.Creational.FactoryMethod;

public class TwigController : Controller
{
    public TwigController(ILogger logger) : base(logger)
    {
    }

    protected override IViewEngine CreateViewEngine()
    {
        var viewEngine = new TwigViewEngine();
        return viewEngine;
    }
}