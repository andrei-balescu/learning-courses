namespace Playstore.Client.Models;

/// <summary>Represents a Bad Request response from a service.</summary>
public class BadRequestDto
{
    /// <summary>The errors returned by the service.</summary>
    public Dictionary<string, IEnumerable<string>> Errors { get; set; }
}