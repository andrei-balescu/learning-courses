namespace OopPrinciples.Coupling.BadExample;

public class BadEmailSender
{
    public void SendEmail(string message)
    {
        Console.WriteLine("Sending email: " + message);
    }
}
