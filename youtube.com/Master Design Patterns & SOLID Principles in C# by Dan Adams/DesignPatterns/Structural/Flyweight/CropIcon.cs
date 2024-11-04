namespace DesignPatterns.Structural.Flyweight;

public class CropIcon
{
    public CropType Type { get; private set; }
    public byte[] Icon { get; private set; }

    public CropIcon(CropType cropType, byte[] icon)
    {
        Type = cropType;
        Icon = icon;
    }
}