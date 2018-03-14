using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = Config.GetUsers().FirstOrDefault(x =>
                    x.Username.ToLower() == context.UserName.ToLower() &&
                    x.Password == context.Password);

            if (user != null)
            {
                context.Result = new GrantValidationResult(user.SubjectId, "custom");
                return Task.FromResult(0);
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
            return Task.FromResult(0);
        }
    }
}