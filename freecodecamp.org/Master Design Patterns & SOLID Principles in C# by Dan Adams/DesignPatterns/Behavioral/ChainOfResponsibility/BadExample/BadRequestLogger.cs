namespace DesignPatterns.Behavioral.ChainOfResponsibility.BadExample;

public class BadRequestLogger
{
    public void Log(HttpRequest request)
    {
        Console.WriteLine("Received request");
    }
}