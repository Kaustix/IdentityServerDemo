using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class DelegationGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _validator;

        public DelegationGrantValidator(ITokenValidator tokenValidator)
        {
            _validator = tokenValidator;
        }

        public string GrantType => "delegation";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var token = context.Request.Raw.Get("token");

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            var result = await _validator.ValidateAccessTokenAsync(token);
            if (result.IsError)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            var sub = result.Claims.FirstOrDefault(x => x.Type == "sub").Value;

            context.Result = new GrantValidationResult(sub, "delegation");
            return;
        }
    }
}