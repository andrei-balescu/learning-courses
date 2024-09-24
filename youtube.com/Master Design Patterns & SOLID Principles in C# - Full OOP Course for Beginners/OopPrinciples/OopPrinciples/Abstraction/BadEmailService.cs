namespace DesignPatternsLibrary.OopPrinciples.Abstraction;

/**
 * Bad example: all workflow methods need to be called by calling service in order to send an email.
 */
public class BadEmailService
{
    public void SendEmail()
    {
        Console.WriteLine("Sending email...");
    }

    public void Connect()
    {
        Console.WriteLine("Connecting to email server...");
    }

    public void Authenticate()
    {
        Console.WriteLine("Authenticating...");
    }

    public void Disconnect()
    {
        Console.WriteLine("Disconnecting from email server...");
    }
}