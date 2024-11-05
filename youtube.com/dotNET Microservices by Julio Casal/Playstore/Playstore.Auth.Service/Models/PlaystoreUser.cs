using Microsoft.AspNetCore.Identity;

namespace Playstore.Auth.Service.Models;

public class PlaystoreUser : IdentityUser
{
    public Guid PlaystoreId { get; set; }
}