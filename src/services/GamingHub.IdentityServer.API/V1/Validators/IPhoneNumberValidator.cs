namespace GamingHub.IdentityServer.API.V1.Validators
{
    /// <summary>
    /// The interface allows to validate a phone number by unique code.
    /// </summary>
    public interface IPhoneNumberValidator
    {
        /// <summary>
        /// Sends a code to specific number.
        /// </summary>
        /// <param name="toPhoneNumber">The phone number to which the message is sent.</param>
        Task<bool> SendCodeAsync(string toPhoneNumber);

        /// <summary>
        /// Returns a flag indicating whether the specified token is valid for the given user.
        /// </summary>
        /// <param name="toPhoneNumber">The phone number to which the message is sent.</param>
        /// <param name="code">The token to validate.</param>
        /// <returns></returns>
        Task<bool> ValidateCodeAsync(string toPhoneNumber, string code);
    }
}
