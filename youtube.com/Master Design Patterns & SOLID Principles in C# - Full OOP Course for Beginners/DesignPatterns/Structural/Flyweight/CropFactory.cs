namespace DesignPatterns.Structural.Flyweight;

public class CropFactory
{
    private ICropIconFactory _cropIconFactory;

    public CropFactory(ICropIconFactory cropIconFactory)
    {
        _cropIconFactory = cropIconFactory;
    }

    public Crop CreateCrop(int x, int y, CropType cropType)
    {
        var cropIcon = _cropIconFactory.GetCropIcon(cropType);
        var crop = new Crop(x, y, cropIcon);
        return crop;
    }

}