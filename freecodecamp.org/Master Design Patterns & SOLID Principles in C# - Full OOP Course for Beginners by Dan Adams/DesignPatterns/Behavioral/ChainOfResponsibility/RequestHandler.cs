namespace DesignPatterns.Behavioral.ChainOfResponsibility;

public abstract class RequestHandler
{
    public RequestHandler? _nextHandler;

    public RequestHandler(RequestHandler? nextHandler = null)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(HttpRequest httpRequest)
    {
        var requestHandlingFinished = DoHandle(httpRequest);
        
        requestHandlingFinished = requestHandlingFinished || _nextHandler == null;

        if (!requestHandlingFinished)
        {
            _nextHandler.Handle(httpRequest);
        }
    }

    protected abstract bool DoHandle(HttpRequest httpRequest);
}