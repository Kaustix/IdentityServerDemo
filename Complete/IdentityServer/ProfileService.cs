using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var id = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var user = Config.GetUsers().FirstOrDefault(x => x.SubjectId == id);

            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("role", user.Claims.FirstOrDefault(x => x.Type == "role").Value)
            };

            context.IssuedClaims.AddRange(claims);

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = Config.GetUsers().FirstOrDefault(x => x.SubjectId == "1").IsActive;

            return Task.FromResult(0);
        }
    }
}