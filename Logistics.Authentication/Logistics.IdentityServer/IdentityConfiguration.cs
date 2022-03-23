using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<Client> Clients
            => new List<Client>
            {
                new Client
                {
                    ClientId = "Logistics.Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Logistics.API"
                    },
                    AllowAccessTokensViaBrowser = true,
                }
            };

        public static IEnumerable<ApiResource> ApiResources
            => new List<ApiResource>
            {
                new ApiResource("Logistics.API", new []{JwtClaimTypes.Name})
                {
                    Scopes = {"Logistics.API" }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes
            => new List<ApiScope>
            {
                new ApiScope("Logistics.API")
            };
    }
}
