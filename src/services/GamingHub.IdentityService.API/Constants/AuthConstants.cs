namespace GamingHub.IdentityService.API.Constants
{
    /// <summary>
    /// The class provides information about authentication constants.
    /// </summary>
    public class AuthConstants
    {
        /// <summary>
        /// Phone number parameter in token request.
        /// </summary>
        public const string PHONE_NUMBER = "phone_number";

        /// <summary>
        /// Verification token parameter in token request.
        /// </summary>
        public const string VERIFICATION_TOKEN = "verification_token";

        /// <summary>
        /// Custom phone number grant type.
        /// </summary>
        public const string GRANT_TYPE_PHONE_NUMBER_TOKEN = "phone_number_token";

        /// <summary>
        /// Password grant type.
        /// </summary>
        public const string PASSWORD_GRANT_TYPE = "password";

        /// <summary>
        /// The type of confirmation token.
        /// </summary>
        public const string CONFIRMATION_BY_SMS = "sms";

        /// <summary>
        /// The redis key that indicates count sms attempts.
        /// </summary>
        public const string COUNT_SMS_ATTEMPT_KEY = "{0}:phones:{1}:sms-attempts"; //{0} - application name {1} - phone number

        /// <summary>
        /// The redis key that indicates sms code.
        /// </summary>
        public const string SMS_CODE_KEY = "phones:{0}:sms-code"; // {0} - phone number

        /// <summary>
        /// The default count sms attempt.
        /// </summary>
        public const int DEFAULT_COUNT_SMS_ATTEMPT = 10;
    }
}
