using Ardalis.GuardClauses;
using GamingHub.IdentityServer.API.Configuration;
using GamingHub.IdentityServer.API.V1.Providers;
using Microsoft.Extensions.Options;

namespace GamingHub.IdentityServer.API.V1.Validators.Implementation
{
    /// <summary>
    /// The interface allows to validate a phone number via SMS code using Twilio Verify SDK.
    /// <see href="https://www.twilio.com/en-us/user-authentication-identity/verify"/>
    /// </summary>
    public class TwilioVerifyPhoneNumberValidator : IPhoneNumberValidator
    {
        private readonly ITwilioProvider _twilioService;
        private readonly TwilioOptions _twilioOptions;
        private readonly ILogger<TwilioVerifyPhoneNumberValidator> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioVerifyPhoneNumberValidator"/> class.
        /// </summary>
        public TwilioVerifyPhoneNumberValidator(
            ITwilioProvider twilioService,
            IOptions<TwilioOptions> twilioOptions,
            ILogger<TwilioVerifyPhoneNumberValidator> logger)
        {
            _twilioService = Guard.Against.Null(twilioService, nameof(twilioService));
            _twilioOptions = Guard.Against.Null(twilioOptions, nameof(twilioOptions)).Value;
            _logger = Guard.Against.Null(logger, nameof(logger));
        }

        #region Implementation of IPhoneNumberValidator

        /// <summary>
        /// Sends an SMS code to specific number using Twilio Verify SDK.
        /// <see href="https://www.twilio.com/docs/verify/api#step-2-send-a-verification-token"/>
        /// </summary>
        /// <param name="toPhoneNumber">The phone number.</param>
        public async Task<bool> SendCodeAsync(string toPhoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_twilioOptions.PathServiceSid))
                {
                    return false;
                }

                return await _twilioService.SendVerificationCodeAsync(
                    _twilioOptions.PathServiceSid,
                    toPhoneNumber);
            }
            catch (Exception exp)
            {
                using (_logger.BeginScope(new Dictionary<string, object>
                       {
                           { nameof(toPhoneNumber), toPhoneNumber },
                       }))
                {
                    _logger.LogError(exp, nameof(SendCodeAsync));
                }

                return false;
            }
        }

        /// <summary>
        /// Checks the one-time passcode sent to the user using Twilio Verify SDK.
        /// The provided code is correct if the response 'status' parameter is 'approved'.
        /// <see href="https://www.twilio.com/docs/verify/api#step-3-check-the-verification-token"/>
        /// </summary>
        /// <param name="toPhoneNumber">The phone number.</param>
        /// <param name="code">The provided verification code.</param>
        /// <returns></returns>
        public async Task<bool> ValidateCodeAsync(string toPhoneNumber, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_twilioOptions.PathServiceSid))
                {
                    return false;
                }

                return await _twilioService.CheckVerificationCodeAsync(
                    _twilioOptions.PathServiceSid,
                    toPhoneNumber,
                    code);
            }
            catch (Exception exp)
            {
                using (_logger.BeginScope(new Dictionary<string, object>
                       {
                           { nameof(toPhoneNumber), toPhoneNumber },
                           { nameof(code), code },
                       }))
                {
                    _logger.LogError(exp, nameof(ValidateCodeAsync));
                }

                return false;
            }
        }

        #endregion
    }
}