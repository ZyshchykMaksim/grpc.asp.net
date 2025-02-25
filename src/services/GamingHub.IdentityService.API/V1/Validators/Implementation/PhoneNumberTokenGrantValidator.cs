using Ardalis.GuardClauses;
using AutoMapper;
using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using EasyCaching.Core;
using GamingHub.IdentityService.API.Configuration;
using GamingHub.IdentityService.API.Constants;
using GamingHub.IdentityService.API.V1.Helpers;
using GamingHub.Service.Shared.Extensions;
using GamingHub.Service.Shared.Providers;
using GamingHub.UserService.gRPC.V1;
using Microsoft.Extensions.Options;
using UserServiceClient = GamingHub.UserService.gRPC.V1.UserService.UserServiceClient;

namespace GamingHub.IdentityService.API.V1.Validators.Implementation
{
    /// <summary>
    /// Custom PhoneNumberToken grant flow.
    /// </summary>
    public class PhoneNumberTokenGrantValidator : IExtensionGrantValidator
    {
        private readonly UserServiceClient _userServiceClient;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPhoneNumberValidator _phoneNumberValidator;
        private readonly IRedisCachingProvider _redisCachingProvider;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ILogger<PhoneNumberTokenGrantValidator> _logger;
        
        /// <inheritdoc cref="IExtensionGrantValidator"/>
        public string GrantType => AuthConstants.GRANT_TYPE_PHONE_NUMBER_TOKEN;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberTokenGrantValidator"/> class.
        /// </summary>
        public PhoneNumberTokenGrantValidator(
            UserServiceClient userServiceClient,
            IPhoneNumberValidator phoneNumberValidator,
            IDateTimeProvider dateTimeProvider,
            IEasyCachingProviderFactory cachingProviderFactory,
            IOptions<RedisCacheOptions> redisCacheOptions,
            IMapper mapper,
            IWebHostEnvironment env,
            ILogger<PhoneNumberTokenGrantValidator> logger)
        {
            var redisCacheOptionsVal = Guard.Against.Null(redisCacheOptions, nameof(redisCacheOptions)).Value;
            var cachingProviderFactoryVal = Guard.Against.Null(cachingProviderFactory, nameof(cachingProviderFactory));
            var redisCachingProvider = cachingProviderFactoryVal.GetRedisProvider(redisCacheOptionsVal.Name); 
            
            _redisCachingProvider = Guard.Against.Null(redisCachingProvider, nameof(redisCachingProvider));
            _userServiceClient = Guard.Against.Null(userServiceClient, nameof(userServiceClient));
            _phoneNumberValidator = Guard.Against.Null(phoneNumberValidator, nameof(phoneNumberValidator));
            _dateTimeProvider = Guard.Against.Null(dateTimeProvider, nameof(dateTimeProvider));
            _mapper = Guard.Against.Null(mapper, nameof(mapper));
            _env = Guard.Against.Null(env, nameof(env));
            _logger = Guard.Against.Null(logger, nameof(logger));
        }

        /// <inheritdoc cref="IExtensionGrantValidator"/>
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var raw = context.Request.Raw;
            var credential = raw.Get(OidcConstants.TokenRequest.GrantType);

            if (credential == null || credential != GrantType)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    "invalid phone_number_token credential");

                return;
            }

            var phoneNumber = raw.Get(AuthConstants.PHONE_NUMBER);
            var verificationToken = raw.Get(AuthConstants.VERIFICATION_TOKEN);
            var clientId = raw.Get(OidcConstants.AuthorizeRequest.ClientId);

            if (string.IsNullOrWhiteSpace(verificationToken) ||
                string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(clientId))
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidRequest,
                    $"{AuthConstants.VERIFICATION_TOKEN} and {AuthConstants.PHONE_NUMBER} are required parameters.");

                _logger.LogInformation("Authentication failed, reason: required parameters didn't provide");

                return;
            }

            phoneNumber = phoneNumber.FormatPhoneNumber();

            var user = await _userServiceClient.GetUserByPhoneNumberAsync(new GetUserByPhoneNumberRequest()
            {
                PhoneNumber = phoneNumber
            });
            
            if (user == null)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidRequest,
                    "user not found");

                _logger.LogInformation("Authentication failed, reason: user not found");

                return;
            }

            if (user.Status != UserStatus.Active)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidRequest,
                    "User isn't active");

                _logger.LogInformation("Authentication failed, reason: user is not active");

                return;
            }

            var isNoNeedValidateCode = false;

            if (!_env.IsProduction())
            {
                isNoNeedValidateCode = TestPhoneNumbersHelper.PhoneNumbers.Contains(phoneNumber) &&
                                       TestPhoneNumbersHelper.ValidCodes.Contains(verificationToken);
            }

            var result = isNoNeedValidateCode ||
                         await _phoneNumberValidator.ValidateCodeAsync(phoneNumber, verificationToken);

            if (!result)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidRequest,
                    "invalid verification token");

                _logger.LogInformation($"Authentication failed for token: {verificationToken}, reason: invalid token");

                return;
            }

            context.Result = new GrantValidationResult(
                user.UserId, 
                AuthConstants.CONFIRMATION_BY_SMS);
        }
    }
}
