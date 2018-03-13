using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("test-api", "Test Api")
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "client-credential-client",
                ClientName = "Client Credential Client",
                AllowedGrantTypes = new[] {GrantType.ClientCredentials},
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedScopes = {"test-api"}
            },
            new Client
            {
                ClientId = "resource-owner-client",
                ClientName = "Resource Owner Password",
                AllowedGrantTypes = new[] {GrantType.ResourceOwnerPassword},
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedScopes = {"test-api"}
            },
            new Client
            {
                ClientId = "mvc-client",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = {"http://localhost:5002/signin-oidc"},
                PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static IEnumerable<TestUser> GetUsers() => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "KiernanL",
                Password = "password"
            }
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
