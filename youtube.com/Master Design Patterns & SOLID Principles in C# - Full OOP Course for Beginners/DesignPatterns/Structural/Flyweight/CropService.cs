namespace DesignPatterns.Structural.Flyweight;

public class CropService
{
    private CropIconFactory _cropIconFactory;

    public CropService(CropIconFactory cropIconFactory)
    {
        _cropIconFactory = cropIconFactory;
    }

    public IEnumerable<Crop> GetCrops()
    {
        var cropIcon = _cropIconFactory.GetCropIcon(CropType.Carrot);

        var crop1 = new Crop(1, 3, cropIcon);
        var crop2 = new Crop(1, 4, cropIcon);
        var crop3 = new Crop(1, 5, cropIcon);

        var crops = new[] { crop1, crop2, crop3 };
        return crops;
    }

}