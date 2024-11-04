using Microsoft.Extensions.Logging;

namespace SolidPrinciples.SingleResponsabilityPrinciple;

public class UserService
{
    private IEmailSender _emailSender;

    public UserService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public void Register(User user)
    {
        // Register user logic...

        _emailSender.SendEmail(user.Email, "Welcome to our platform.");
    }
}