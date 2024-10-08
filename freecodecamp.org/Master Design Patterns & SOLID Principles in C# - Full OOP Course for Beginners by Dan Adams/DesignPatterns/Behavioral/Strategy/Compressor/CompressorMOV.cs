using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Strategy.Compressor;

public class CompressorMOV : ICompressor
{
    private ILogger _logger;

    public string FileExtension => "mov";

    public CompressorMOV(ILogger logger)
    {
        _logger = logger;
    }

    public void Compress()
    {
        _logger.LogInformation("Compressing using MOV");
    }
}