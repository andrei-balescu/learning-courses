namespace DesignPatterns.Behavioral.Visitor.BadExample;

public abstract class BadCompany
{
    protected string _name;

    protected string _email;

    public BadCompany(string name, string email)
    {
        _name = name;
        _email = email;
    }

    public abstract void SendEmail();

    public abstract void ExportToPdf();
}