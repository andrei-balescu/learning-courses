namespace DesignPatterns.Structural.Decorator.BadExample;

public class BadCompressedAndEncryptedData : BadCloudData
{
    public BadCompressedAndEncryptedData(string url) : base(url)
    {
    }

    public override void Save(string data)
    {
        Console.WriteLine($"Compressing and encrypting data: {data}");
        base.Save(data);
    }
}