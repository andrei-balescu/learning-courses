namespace SolidPrinciples.SingleResponsabilityPrinciple;

public interface IEmailSender
{
    void SendEmail(string email, string message);
}