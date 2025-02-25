using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace GamingHub.IdentityService.API.V1.Validators.Implementation
{
    /// <summary>
    /// Custom Resource Owner Password Validator
    /// </summary>
    public class PasswordGrantValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordGrantValidator"/> class.
        /// </summary>
        public PasswordGrantValidator() { }

        /// <inheritdoc cref="IResourceOwnerPasswordValidator"/>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(
                TokenRequestErrors.UnauthorizedClient,
                "Invalid Credentials");
            
            return Task.CompletedTask;
        }
    }
}
