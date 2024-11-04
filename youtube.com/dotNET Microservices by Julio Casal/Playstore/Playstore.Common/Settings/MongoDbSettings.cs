namespace Playstore.Common.Settings;

/// <summary>Represents the Mongo DB settings in <c>appsettings.json</c>.</summary>
public class MongoDbSettings
{
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/init
    /// <summary>MongoDB host.</summary>
    public string Host { get; init; }

    /// <summary>MongoDB port.</summary>
    public int Port { get; init; }

    /// <summary>Connection string to MongoDB.</summary>
    public string ConnectionString => $"mongodb://{Host}:{Port}";

    /// <summary>Database name.</summary>
    public string Database { get; init; }
}