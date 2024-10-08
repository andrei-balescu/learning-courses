using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Decorator;

public class CompressionDecorator : DataDecorator
{
    public CompressionDecorator(ILogger logger, IDataStorage dataStorage) : base(logger, dataStorage)
    {
    }

    public override void Save(string data)
    {
        _logger.LogInformation($"Compressing data: {data}");
        _dataStorage.Save(data);
    }
}