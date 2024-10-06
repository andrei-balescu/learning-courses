using DesignPatterns.Structural.Proxy.YouTube;
using Microsoft.Extensions.Logging;

namespace DesignPatterns.Structural.Proxy;

public class YoutubeVideoProxy : IYoutubeVideo
{
    private ILogger _logger;
    private YoutubeVideo? _youtubeVideo;

    public string VideoId { get; private set; }

    public YoutubeVideoProxy(ILogger logger, string videoId)
    {
        _logger = logger;
        VideoId = videoId;
    }

    public void Render()
    {
        if (_youtubeVideo == null)
        {
            _youtubeVideo = new YoutubeVideo(_logger, VideoId);
        }

        _youtubeVideo.Render();
    }
}