using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class IdentityConfiguration
    {
        public static string ScopeAPI
            => "Logistics.API";

        public static IEnumerable<Client> BuildClients(IConfiguration configuration)
        {
            var toReturn = new List<Client>
            {
                new Client
                {
                    ClientId = "APIClient",
                    ClientSecrets = {
                        new Secret("API_super_secert".ToSha256())
                    },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                        ScopeAPI,
                    },
                },
                new Client
                {
                    ClientId = "MVCClient",
                    ClientSecrets =
                    {
                        new Secret("MVC_super_secert".ToSha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        ScopeAPI,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RedirectUris = { configuration.GetSection("MVCBaseUrl").Value + "/signin-oidc"},
                    RequireConsent = false,
                }
            };
            return toReturn;
        }

        public static IEnumerable<ApiResource> ApiResources
            => new List<ApiResource>
            {
                new ApiResource("Logistics.API", new []{JwtClaimTypes.Name,  JwtClaimTypes.Role})
                {
                    Scopes =
                    {
                        "Logistics.API",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },

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
