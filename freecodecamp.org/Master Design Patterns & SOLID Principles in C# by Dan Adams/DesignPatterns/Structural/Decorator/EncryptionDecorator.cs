using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Decorator;

public class EncryptionDecorator : DataDecorator
{
    public EncryptionDecorator(ILogger logger, IDataStorage dataStorage) : base(logger, dataStorage)
    {
    }

    public override void Save(string data)
    {
        _logger.LogInformation($"Encrypting data: {data}");
        _dataStorage.Save(data);
    }
}