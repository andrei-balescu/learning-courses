using DesignPatterns.Behavioral.Visitor.Companies;

namespace DesignPatterns.Behavioral.Visitor;

public abstract class Company
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Company(string name, string email)
    {
        Name = name;
        Email = email; 
    }

    public abstract void Accept(IVisitor visitor);
}