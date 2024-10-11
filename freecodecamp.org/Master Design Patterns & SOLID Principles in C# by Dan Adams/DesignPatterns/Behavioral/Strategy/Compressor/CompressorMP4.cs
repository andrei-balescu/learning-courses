using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Strategy.Compressor;

public class CompressorMP4 : ICompressor
{
    private ILogger _logger;

    public string FileExtension => "mp4";

    public CompressorMP4(ILogger logger)
    {
        _logger = logger;
    }

    public void Compress()
    {
        _logger.LogInformation("Compressing using MP4");
    }

}