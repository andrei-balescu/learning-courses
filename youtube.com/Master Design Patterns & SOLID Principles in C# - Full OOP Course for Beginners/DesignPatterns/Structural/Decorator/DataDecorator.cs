using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Decorator;

public abstract class DataDecorator : IDataStorage
{
    protected ILogger _logger;
    protected IDataStorage _dataStorage;

    protected DataDecorator(ILogger logger, IDataStorage dataStorage)
    {
        _logger = logger;
        _dataStorage = dataStorage;
    }

    public abstract void Save(string data);
}