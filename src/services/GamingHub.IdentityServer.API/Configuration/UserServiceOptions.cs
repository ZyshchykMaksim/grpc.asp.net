namespace GamingHub.IdentityServer.API.Configuration;

/// <summary>
/// The class provides information about options for user service.
/// </summary>
public class UserServiceOptions
{
    public const string NAME_KEY = "UserService";

    /// <summary>
    /// Gets or sets the connection endpoint for user service.
    /// </summary>
    public string Endpoint { get; set; }
}