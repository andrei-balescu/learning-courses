namespace SolidPrinciples.SingleResponsabilityPrinciple.BadExample;

public class BadEmailSender
{
    public void SendEmail(string email, string message)
    {
        Console.WriteLine($"Sending email to {email}: {message}");
    }
}