using DesignPatterns.Structural.Adapter.Lib3rdParty;

namespace DesignPatterns.Structural.Adapter;

public class RainbowFilter : IVideoFilter
{
    private Rainbow _rainbow;

    public RainbowFilter(Rainbow rainbow)
    {
        _rainbow = rainbow;
    }

    public void Apply(Video video)
    {
        _rainbow.Update(video);
    }
}