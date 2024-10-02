using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Observer;

public class Spreadsheet : IObserver<IEnumerable<int>>
{
    private ILogger _logger;

    public Spreadsheet(ILogger logger)
    {
        _logger = logger;
    }

    public void Update(IEnumerable<int> payload)
    {
        int total = payload.Sum();

        _logger.LogInformation($"Values total is {total}");
    }
}