namespace DesignPatterns.Behavioral.ChainOfResponsibility;

public class WebServer
{
    private RequestHandler _requestHandler;

    public WebServer(RequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public void Handle(HttpRequest httpRequest)
    {
        _requestHandler.Handle(httpRequest);
    }
}