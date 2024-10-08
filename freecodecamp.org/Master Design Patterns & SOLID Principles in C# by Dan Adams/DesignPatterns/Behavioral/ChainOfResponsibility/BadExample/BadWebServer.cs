namespace DesignPatterns.Behavioral.ChainOfResponsibility.BadExample;

public class BadWebServer
{
    public void HandleRequest(HttpRequest httpRequest)
    {
        var validator = new BadRequestValidator();
        validator.Validate(httpRequest);

        var authenticator = new BadRequestAuthenticator();
        authenticator.Authenticate(httpRequest);

        var logger = new BadRequestLogger();
        logger.Log(httpRequest);
    }
}