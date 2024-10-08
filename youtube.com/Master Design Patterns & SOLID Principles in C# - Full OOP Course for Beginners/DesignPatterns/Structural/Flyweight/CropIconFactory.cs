namespace DesignPatterns.Structural.Flyweight;

public class CropIconFactory : ICropIconFactory
{
    private IDictionary<CropType, CropIcon> _cropIcons = new Dictionary<CropType, CropIcon>();

    public CropIcon GetCropIcon(CropType cropType)
    {
        CropIcon cropIcon;
        if (!_cropIcons.ContainsKey(cropType))
        {
            var cropIconImage = new byte[0];
            cropIcon = new CropIcon(cropType, cropIconImage);
            _cropIcons.Add(cropType, cropIcon);
        }
        else
        {
            cropIcon = _cropIcons[cropType];
        }

        return cropIcon;
    }
}