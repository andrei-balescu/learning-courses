using Microsoft.Extensions.Logging;

namespace DesignPatterns.Behavioral.Strategy;

/**
 * Strategy Pattern: Context component
 */
public class VideoStorage
{
    private ILogger _logger;
    private ICompressor _compressor;
    private IOverlay _overlay;

    public VideoStorage(ILogger logger, ICompressor compressor, IOverlay overlay = null)
    {
        _logger = logger;
        _compressor = compressor;
        _overlay = overlay;
    }

    public void Store(string fileName)
    {
        _compressor.Compress();
        if (_overlay != null)
        {
            _overlay.Apply();
        }

        _logger.LogInformation($"Storing video to {fileName}.{_compressor.FileExtension}");
    } 
}