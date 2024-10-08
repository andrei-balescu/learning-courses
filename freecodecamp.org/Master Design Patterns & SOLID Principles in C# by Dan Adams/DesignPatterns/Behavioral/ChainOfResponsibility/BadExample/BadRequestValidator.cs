namespace DesignPatterns.Behavioral.ChainOfResponsibility.BadExample;

public class BadRequestValidator
{
    public void Validate (HttpRequest httpRequest)
    {
        string username = httpRequest.Username;
        string password = httpRequest.Password;

        // Trim whitespace
        httpRequest.Username = username.Trim();
        httpRequest.Password = password.Trim();
    }
}