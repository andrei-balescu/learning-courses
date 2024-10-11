namespace SolidPrinciples.SingleResponsabilityPrinciple.BadExample;

/**
 * Bad example: User class contains both user data and registration logic: has multiple reasons for change.
 */
public class BadUser
{
    public string Username { get; set; }
    public string Email { get; set; }

    public void Register()
    {
        // Register user logic...

        var emailSender = new BadEmailSender();
        emailSender.SendEmail(Email, "Welcome to our platform.");
    }
}