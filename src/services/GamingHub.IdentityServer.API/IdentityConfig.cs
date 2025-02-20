using Duende.IdentityServer.Models;
using GamingHub.IdentityServer.API.Constants;

namespace GamingHub.IdentityServer.API;

public class IdentityConfig
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("gaminghub-api", "Gaming Hub API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "b5e41209-3e0e-4fb7-944d-e1e910adc540",
                AllowedGrantTypes = new List<string>()
                {
                    GrantType.ClientCredentials,
                    AuthConstants.GRANT_TYPE_PHONE_NUMBER_TOKEN
                },
                ClientSecrets = { new Secret("91e5ae65-b5f6-40d3-978f-5fb3d16bb78c".Sha256()) },
                AllowedScopes = { "gaminghub-api" },
                AllowOfflineAccess = true,
                AccessTokenLifetime = 157680000
            }
        };
}