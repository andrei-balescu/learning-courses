namespace DesignPatterns.Structural.Flyweight.BadExample;

public class BadCrop
{
    private int _x; // 4 bytes

    private int _y; // 4 bytes
    private CropType _cropType; // 4 bytes
    private byte[] _icon; // 40kb -> 40mb for 1000 items

    public BadCrop(int x, int y, CropType cropType, byte[] icon)
    {
        _x = x;
        _y = y;
        _cropType = cropType;
        _icon = icon;
    }

    /// <summary>
    /// Render crop on screen.
    /// </summary>
    public void Render()
    {
        Console.WriteLine($"Drawing {_cropType} at ({_x}, {_y})");
    }
}