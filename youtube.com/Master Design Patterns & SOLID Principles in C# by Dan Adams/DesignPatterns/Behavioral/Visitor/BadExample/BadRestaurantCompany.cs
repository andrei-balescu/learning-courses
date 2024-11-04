namespace DesignPatterns.Behavioral.Visitor.BadExample;

public class BadRestaurantCompany : BadCompany
{
    public BadRestaurantCompany(string name, string email) : base(name, email)
    {
    }

    public override void ExportToPdf()
    {
        Console.WriteLine($"Exporting restaurant data to PDF for {_name}");
    }

    public override void SendEmail()
    {
        Console.WriteLine($"Sending restaurant marketing tips email to {_email}");
    }
}