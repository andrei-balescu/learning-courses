namespace DesignPatterns.Behavioral.Strategy.BadExample;

public class BadVideoStorage
{
    private BadCompressors _compressor;
    private BadOverlays _overlay;

    public BadVideoStorage(BadCompressors compressor, BadOverlays overlay = BadOverlays.None)
    {
        _compressor = compressor;
        _overlay = overlay;
    }

    public void Store(string fileName)
    {
        // compression logic
        if (_compressor == BadCompressors.MOV)
        {
            Console.WriteLine("Compressing using MOV");
        }
        else if (_compressor == BadCompressors.MP4)
        {
            Console.WriteLine("Compressing using MP4");
        }
        else if (_compressor == BadCompressors.WebM)
        {
            Console.WriteLine("Compressing using WEBM");
        }

        // overlay logic
        if (_overlay == BadOverlays.BlackAndWhite)
        {
            Console.WriteLine("Applying black and white overlay");
        }
        else if (_overlay == BadOverlays.Blur)
        {
            Console.WriteLine("Applying blur overlay");
        }
        else if (_overlay == BadOverlays.None)
        {
            Console.WriteLine("Not applying an overlay");
        }

        Console.WriteLine($"Storing video to {fileName}.{_compressor}");
    }
}