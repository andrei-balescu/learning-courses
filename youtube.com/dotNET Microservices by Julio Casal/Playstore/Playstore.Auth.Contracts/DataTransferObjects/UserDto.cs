namespace Playstore.Auth.Contracts.DataTransferObjects;

/// <summary>DTO containing information about a user.</summary>
/// <param name="Id">The user ID.</param>
/// <param name="Name">The user login name.</param>
public record UserDto(Guid Id, string Name);