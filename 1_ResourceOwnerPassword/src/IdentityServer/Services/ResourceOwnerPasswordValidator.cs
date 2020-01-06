using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace IdentityServer.Services
{
    /// <summary>
    /// Handles validation of resource owner password credentials
    /// </summary>
    public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        private readonly IAuthenticationService _authenticationService;

        public ResourceOwnerPasswordValidator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            TestUser user = _authenticationService.ValidateUser(context.UserName, context.Password);

            if (user == null)
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "The username and password do not match");
            else
                context.Result = new GrantValidationResult(user.Guid.ToString(), "password");

            return Task.FromResult(context.Result);
        }
    }
}