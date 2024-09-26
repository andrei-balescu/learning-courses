namespace OopPrinciples.Coupling.BadExample;

public class BadOrder
{
    public void PlaceOrder()
    {
        // Tight coupling: Order directly creates an EmailSeender itself.
        BadEmailSender emailSender = new BadEmailSender();
        emailSender.SendEmail("Order placed successfullty.");
    }
}

