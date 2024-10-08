using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Decorator;

public class CloudData : IDataStorage
{
    private ILogger _logger;
    private readonly string _url;

    public CloudData(ILogger logger, string url)
    {
        _logger = logger;
        _url = url;
    }

    public void Save(string data)
    {
        _logger.LogInformation($"Saving data: {data} to cloud at {_url}");
    }
}