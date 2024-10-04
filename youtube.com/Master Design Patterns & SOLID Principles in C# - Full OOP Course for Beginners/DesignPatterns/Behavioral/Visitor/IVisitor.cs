using DesignPatterns.Behavioral.Visitor.Companies;

namespace DesignPatterns.Behavioral.Visitor;

public interface IVisitor
{
    void Visit(LawCompany lawCompany);

    void Visit(RestaurantCompany restaurantCompany);
}