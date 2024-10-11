using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Proxy.YouTube;

/// <summary>
/// 3rd party Youtube API that downloads the vidio upon creating the video object.
/// </summary>
public class YoutubeVideo : IYoutubeVideo
{
    private ILogger _logger;

    public string VideoId { get; private set; }

    public YoutubeVideo(ILogger logger, string videoId)
    {
        _logger = logger;
        VideoId = videoId;
        Download();
    }

    public void Render()
    {
        _logger.LogInformation($"Rendering video {VideoId}");
    }

    private void Download()
    {
        _logger.LogInformation($"Downloading video with id {VideoId} from YouTube API");
    }
}