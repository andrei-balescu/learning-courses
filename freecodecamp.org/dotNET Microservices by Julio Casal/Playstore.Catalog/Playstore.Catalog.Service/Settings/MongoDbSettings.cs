namespace Playstore.Catalog.Service.Settings;

public class MongoDbSettings
{
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/init
    public string Host { get; init; }
    public int Port { get; init; }
    public string ConnectionString => $"mongodb://{Host}:{Port}";
    public string Database { get; init; }
}