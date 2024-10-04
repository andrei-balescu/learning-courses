namespace DesignPatterns.Behavioral.Visitor.Companies;

public class RestaurantCompany : Company
{
    public RestaurantCompany(string name, string email) : base(name, email)
    {
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}