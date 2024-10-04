using DesignPatterns.Behavioral.Visitor.Companies;
using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Visitor.CompanyServices;

public class EmailService : IVisitor
{
    private ILogger _logger;

    public EmailService(ILogger logger)
    {
        _logger = logger;
    }

    public void Visit(LawCompany lawCompany)
    {
        _logger.LogInformation($"Sending law marketing tips email to {lawCompany.Email}");
    }

    public void Visit(RestaurantCompany restaurantCompany)
    {
        _logger.LogInformation($"Sending restaurant marketing tips email to {restaurantCompany.Email}");
    }
}