namespace DesignPatterns.Structural.Decorator.BadExample;

public class BadEncryptedData : BadCloudData
{
    public BadEncryptedData(string url) : base(url)
    {
    }

    public override void Save(string data)
    {
        Console.WriteLine($"Encrypting data: {data}");
        base.Save(data);
    }
}