using DesignPatterns.Behavioral.Visitor.Companies;
using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Visitor.CompanyServices;

public class PdfExportService : IVisitor
{
    private ILogger _logger;

    public PdfExportService(ILogger logger)
    {
        _logger = logger;
    }

    public void Visit(LawCompany lawCompany)
    {
        _logger.LogInformation($"Exporting law data to PDF for {lawCompany.Name}");
    }

    public void Visit(RestaurantCompany restaurantCompany)
    {
        _logger.LogInformation($"Exporting restaurant data to PDF for {restaurantCompany.Name}");
    }
}