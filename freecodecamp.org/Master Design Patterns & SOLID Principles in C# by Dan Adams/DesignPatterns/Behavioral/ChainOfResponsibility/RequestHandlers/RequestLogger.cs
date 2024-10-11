using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.ChainOfResponsibility.RequestHandlers;

public class RequestLogger : RequestHandler
{
    private ILogger _logger;

    public RequestLogger(ILogger logger, RequestHandler? requestHandler = null) : base(requestHandler)
    {
        _logger = logger;
    }

    protected override bool DoHandle(HttpRequest httpRequest)
    {
        _logger.LogInformation("Request received.");

        bool stopHandling = false;
        return stopHandling;
    }
}