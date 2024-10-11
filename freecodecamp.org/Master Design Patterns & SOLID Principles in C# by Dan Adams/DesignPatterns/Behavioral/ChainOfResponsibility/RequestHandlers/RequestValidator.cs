namespace DesignPatterns.Behavioral.ChainOfResponsibility.RequestHandlers;

public class RequestValidator : RequestHandler
{
    public RequestValidator(RequestHandler? requestHandler = null) : base(requestHandler)
    {

    }

    protected override bool DoHandle(HttpRequest httpRequest)
    {
        string username = httpRequest.Username;
        string password = httpRequest.Password;

        // Trim whitespace
        httpRequest.Username = username.Trim();
        httpRequest.Password = password.Trim();

        bool stopHandling = string.IsNullOrEmpty(httpRequest.Username)
                            || string.IsNullOrEmpty(httpRequest.Password);

        return stopHandling;
    }
}