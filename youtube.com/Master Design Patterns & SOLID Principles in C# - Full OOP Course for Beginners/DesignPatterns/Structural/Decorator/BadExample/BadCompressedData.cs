namespace DesignPatterns.Structural.Decorator.BadExample;

public class BadCompressedData : BadCloudData
{
    public BadCompressedData(string url) : base(url)
    {
    }

    public override void Save(string data)
    {
        Console.WriteLine($"Compressing data: {data}");
        base.Save(data);
    }
}