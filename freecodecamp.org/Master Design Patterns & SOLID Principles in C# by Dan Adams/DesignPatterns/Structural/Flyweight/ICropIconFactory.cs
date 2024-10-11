namespace DesignPatterns.Structural.Flyweight;

public interface ICropIconFactory
{
    CropIcon GetCropIcon(CropType cropType);
}