namespace DesignPatterns.Behavioral.ChainOfResponsibility.RequestHandlers;

public class RequestAuthenticator : RequestHandler
{
    public RequestAuthenticator(RequestHandler? requestHandler = null) : base(requestHandler)
    {

    }

    protected override bool DoHandle(HttpRequest httpRequest)
    {
        bool isAuthenticated = httpRequest.Username == "GoodUser" 
                                && httpRequest.Password == "GoodPassword";

        return !isAuthenticated;
    }
}