namespace DesignPatterns.Structural.Proxy.YouTube;

public interface IYoutubeVideo
{
    string VideoId { get; }

    void Render();
}

