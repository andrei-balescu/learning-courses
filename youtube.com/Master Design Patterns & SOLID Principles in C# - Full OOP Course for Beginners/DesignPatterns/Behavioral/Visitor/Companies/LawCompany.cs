namespace DesignPatterns.Behavioral.Visitor.Companies;

public class LawCompany : Company
{
    public LawCompany(string name, string email) : base(name, email)
    {
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}