using DesignPatterns.Structural.Proxy.YouTube;

namespace DesignPatterns.Structural.Proxy;

public class VideoList
{
    private IDictionary<string, IYoutubeVideo> _videos = new Dictionary<string, IYoutubeVideo>();

    public void Add(IYoutubeVideo video)
    {
        _videos.Add(video.VideoId, video);
    }

    public void Watch(string videoId)
    {
        IYoutubeVideo video = _videos[videoId];
        video.Render();
    }
}