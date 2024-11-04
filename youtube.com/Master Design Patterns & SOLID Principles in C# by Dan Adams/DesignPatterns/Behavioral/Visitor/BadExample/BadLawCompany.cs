namespace DesignPatterns.Behavioral.Visitor.BadExample;

public class BadLawCompany : BadCompany
{
    public BadLawCompany(string name, string email) : base(name, email)
    {
    }

    public override void ExportToPdf()
    {
        Console.WriteLine($"Exporting law data to PDF for {_name}");
    }

    public override void SendEmail()
    {
        Console.WriteLine($"Sending law marketing tips email to {_email}");
    }
}