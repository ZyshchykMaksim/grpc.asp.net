using Ardalis.GuardClauses;
using EasyCaching.Core;
using GamingHub.IdentityService.API.Configuration;
using GamingHub.IdentityService.API.Constants;
using GamingHub.IdentityService.API.V1.Providers;
using GamingHub.Service.Shared.Utils;
using Microsoft.Extensions.Options;

namespace GamingHub.IdentityService.API.V1.Validators.Implementation
{
    /// <summary>
    /// The interface allows to validate a phone number via SMS code using Twilio Massaging SDK.
    /// <see href="https://www.twilio.com/en-us/messaging/channels/sms"/>
    /// </summary>
    public class TwilioSmsPhoneNumberValidator : IPhoneNumberValidator
    {
        private readonly ITwilioProvider _twilioService;
        private readonly IRedisCachingProvider _redisCacheProvider;
        private readonly TwilioOptions _twilioOptions;
        private readonly ILogger<TwilioSmsPhoneNumberValidator> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioSmsPhoneNumberValidator"/> class.
        /// </summary>
        public TwilioSmsPhoneNumberValidator(
            ITwilioProvider twilioService,
            IEasyCachingProviderFactory cachingProviderFactory,
            IOptions<RedisCacheOptions> redisCacheOptions,
            IOptions<TwilioOptions> twilioOptions,
            ILogger<TwilioSmsPhoneNumberValidator> logger)
        {
            var redisCacheOptionsVal = Guard.Against.Null(redisCacheOptions, nameof(redisCacheOptions)).Value;
            var cachingProviderFactoryVal = Guard.Against.Null(cachingProviderFactory, nameof(cachingProviderFactory));
            var redisCacheProvider = cachingProviderFactoryVal.GetRedisProvider(redisCacheOptionsVal.Name);

            _twilioService = Guard.Against.Null(twilioService, nameof(twilioService));
            _redisCacheProvider = Guard.Against.Null(redisCacheProvider, nameof(redisCacheProvider));
            _twilioOptions = Guard.Against.Null(twilioOptions, nameof(twilioOptions)).Value;
            _logger = Guard.Against.Null(logger, nameof(logger));
        }

        #region Implementation of IPhoneNumberValidator

        /// <summary>
        /// Sends an SMS code to specific number.
        /// </summary>
        /// <param name="toPhoneNumber">The phone number to which the message is sent.</param>
        public async Task<bool> SendCodeAsync(string toPhoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toPhoneNumber))
                {
                    return false;
                }

                if (_twilioOptions.PhoneNumbers.Count <= 0)
                {
                    return false;
                }

                var phoneNumbers = _twilioOptions.PhoneNumbers.ToArray();
                var probabilities = phoneNumbers
                    .Select(_ => (decimal)100 / phoneNumbers.Length)
                    .ToArray();
                var fromPhoneNumber = ProbabilityGenerator.Randomize(
                    phoneNumbers,
                    probabilities);

                var code = UniqKey.GenerateEx(
                    size: 6,
                    useLowercase: false,
                    useUppercase: false,
                    useNumbers: true,
                    useSpecial: false);
                var sendResult = await _twilioService.SendSmsAsync(
                    fromPhoneNumber,
                    toPhoneNumber,
                    string.Format(_twilioOptions.AuthCodeMessage, code));

                if (sendResult)
                {
                    return await _redisCacheProvider.StringSetAsync(
                        string.Format(AuthConstants.SMS_CODE_KEY, toPhoneNumber),
                        code,
                        TimeSpan.FromHours(2));
                }

                return false;
            }
            catch (Exception exp)
            {
                using (_logger.BeginScope(new Dictionary<string, object>
                       {
                           { nameof(toPhoneNumber), toPhoneNumber }
                       }))
                {
                    _logger.LogError(exp, nameof(SendCodeAsync));
                }

                return false;
            }
        }

        /// <summary>
        /// Checks the one-time passcode sent to the user.
        /// The provided code is correct if the response approved.
        /// </summary>
        /// <param name="toPhoneNumber">The phone number to which the message is sent.</param>
        /// <param name="code">The provided verification code.</param>
        /// <returns></returns>
        public async Task<bool> ValidateCodeAsync(string toPhoneNumber, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toPhoneNumber) ||
                    string.IsNullOrWhiteSpace(code))
                {
                    return false;
                }

                var cacheKey = string.Format(AuthConstants.SMS_CODE_KEY, toPhoneNumber);

                var strCode = await _redisCacheProvider.StringGetAsync(cacheKey);

                if (string.IsNullOrWhiteSpace(strCode))
                {
                    return false;
                }

                await _redisCacheProvider.KeyDelAsync(cacheKey);

                return strCode.Equals(code);
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