using DesignPatterns.Behavioral.ChainOfResponsibility;

namespace DesignPatternsTest.Behavioral.ChainOfResponsibility;

/// <summary>
/// Used to verify that handlers pass the object to next handler in chain
/// </summary>
public class MockRequestHandler : RequestHandler
{
    protected sealed override bool DoHandle(HttpRequest httpRequest)
    {
        return MockHandle(httpRequest);
    }

    public virtual bool MockHandle(HttpRequest httpRequest)
    {
        bool stopProcessing = false;
        return stopProcessing;
    }
}