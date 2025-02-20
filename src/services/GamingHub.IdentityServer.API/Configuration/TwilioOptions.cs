namespace GamingHub.IdentityServer.API.Configuration
{
    /// <summary>
    /// The class provides information about options for Twilio.
    /// </summary>
    public class TwilioOptions
    {
        public const string NAME_KEY = "Twilio";

        /// <summary>
        /// Gets or sets the Twilio account SID.
        /// </summary>
        public string AccountSid { get; set; }

        /// <summary>
        /// Gets sets the Twilio auth token.
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Get sets the path service SID
        /// </summary>
        public string PathServiceSid { get; set; }
        
        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        public HashSet<string> PhoneNumbers { get; set; } = [];
        
        /// <summary>
        /// Gets or sets a message that will be sent to the user to confirm the account.
        /// </summary>
        public string AuthCodeMessage { get; set; } = "Your verification code is: {0}.";
    }
}