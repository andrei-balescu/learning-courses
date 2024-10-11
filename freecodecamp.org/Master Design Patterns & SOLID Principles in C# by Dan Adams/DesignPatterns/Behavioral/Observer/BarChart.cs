using DesignPatterns.Behavioral.Observer;
using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Observer;

public class BarChart : IDataSourceObserver<IEnumerable<int>>
{
    private ILogger _logger;

    public BarChart(ILogger logger)
    {
        _logger = logger;
    }

    public void Update(IEnumerable<int> payload)
    {
        var numberOfValues = payload.Count();
        _logger.LogInformation($"Rendering bar chart with {numberOfValues} values");
    }
}