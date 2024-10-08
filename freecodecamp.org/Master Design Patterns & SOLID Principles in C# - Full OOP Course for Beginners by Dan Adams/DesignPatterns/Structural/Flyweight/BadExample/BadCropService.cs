namespace DesignPatterns.Structural.Flyweight.BadExample;

public class BadCropService
{
    /// <summary>Get From DB.</summary>
    /// <returns>A list of <c>Crop</c> items</returns>
    public IEnumerable<BadCrop> GetCrops()
    {
        var crop1 = new BadCrop(1, 3, CropType.Carrot, new byte[0]);
        var crop2 = new BadCrop(1, 4, CropType.Carrot, new byte[0]);
        var crop3 = new BadCrop(1, 5, CropType.Carrot, new byte[0]);

        var crops = new[] { crop1, crop2, crop3 };
        return crops;
    }
}