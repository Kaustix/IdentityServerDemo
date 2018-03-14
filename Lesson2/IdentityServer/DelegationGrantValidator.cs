//using IdentityServer4.Models;
//using IdentityServer4.Validation;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IdentityServer
//{
//    public class DelegationGrantValidator : IExtensionGrantValidator
//    {
//        private readonly ITokenValidator _validator;

//        public DelegationGrantValidator(ITokenValidator tokenValidator)
//        {
//            _validator = tokenValidator;
//        }

//        public string GrantType => "delegation";

//        public async Task ValidateAsync(ExtensionGrantValidationContext context)
//        {
//        }
//    }
//}