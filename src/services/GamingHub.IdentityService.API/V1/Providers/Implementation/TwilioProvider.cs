using Ardalis.GuardClauses;
using GamingHub.IdentityService.API.Configuration;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;

namespace GamingHub.IdentityService.API.V1.Providers.Implementation
{
    /// <summary>
    /// The class allows to use Twilio's features.
    /// <see href="https://www.twilio.com/en-us"/>
    /// </summary>
    public class TwilioProvider : ITwilioProvider
    {
        private readonly ILogger<TwilioProvider> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioProvider"/> class.
        /// </summary>
        public TwilioProvider(
            IOptions<TwilioOptions> twilioOptions,
            ILogger<TwilioProvider> logger)
        {
            var twilioOptions1 = Guard.Against.Null(twilioOptions.Value, nameof(twilioOptions));
            _logger = Guard.Against.Null(logger, nameof(logger));

            TwilioClient.Init(twilioOptions1.AccountSid, twilioOptions1.AuthToken);
        }

        #region Implementation of ITwilioService

        /// <inheritdoc />
        public async Task<bool> SendVerificationCodeAsync(string serviceSid, string toPhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(serviceSid))
            {
                return false;
            }

            var result = await VerificationResource.CreateAsync(
                  to: toPhoneNumber,
                  channel: "sms",
                  pathServiceSid: serviceSid);

            return result != null &&
                   string.Equals(result.ServiceSid, serviceSid);
        }

        /// <inheritdoc />
        public async Task<bool> CheckVerificationCodeAsync(string serviceSid, string toPhoneNumber, string code)
        {
            if (string.IsNullOrWhiteSpace(serviceSid))
            {
                return false;
            }

            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: toPhoneNumber,
                code: code,
                pathServiceSid: serviceSid);

            return verificationCheck.Valid ?? false;
        }

        /// <inheritdoc />
        public async Task<bool> SendSmsAsync(string fromPhoneNumber, string toPhoneNumber, string msgBody)
        {
            if (string.IsNullOrWhiteSpace(fromPhoneNumber) ||
                string.IsNullOrWhiteSpace(toPhoneNumber) ||
                string.IsNullOrWhiteSpace(msgBody))
            {
                return false;
            }

            var messageResult = await MessageResource.CreateAsync(
            body: msgBody,
            from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
            to: new Twilio.Types.PhoneNumber(toPhoneNumber));

            if (messageResult.ErrorCode.HasValue ||
                !string.IsNullOrWhiteSpace(messageResult.ErrorMessage))
            {
                using (_logger.BeginScope(new Dictionary<string, object>
                {
                    [nameof(fromPhoneNumber)] = fromPhoneNumber,
                    [nameof(toPhoneNumber)] = toPhoneNumber,
                    [nameof(msgBody)] = msgBody
                }))
                {
                    _logger.LogError(string.Format(
                        format: "ErrorCode:{0}, ErrorMessage:{1}",
                        arg0: messageResult.ErrorCode ?? -1,
                        arg1: messageResult.ErrorMessage ?? ""));
                }

                return false;
            }

            return true;
        }

        #endregion
    }
}
