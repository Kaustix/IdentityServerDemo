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
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
        };

        public static IEnumerable<TestUser> GetUsers() => new List<TestUser>
        {
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
        };
    }
}
