namespace DesignPatterns.Structural.Decorator.BadExample;

public class BadCloudData
{
    protected string _url;

    public BadCloudData(string url)
    {
        _url = url;
    }

    public virtual void Save(string data)
    {
        Console.WriteLine($"Saving data: {data} to cloud at {_url}");
    }
}