namespace DesignPatterns.Behavioral.ChainOfResponsibility.BadExample;

public class BadRequestAuthenticator
{
    public bool Authenticate(HttpRequest httpRequest)
    {
        bool isAuthenticated = httpRequest.Username == "danny" 
                                && httpRequest.Password == "123";
        return isAuthenticated;
    }
}