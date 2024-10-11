namespace DesignPatterns.Structural.Adapter;

public class VideoEditor
{
    private Video _video;

    public VideoEditor(Video video)
    {
        _video = video;
    }

    public void ApplyFilter(IColorFilter filter)
    {
        filter.Apply(_video);
    }
}