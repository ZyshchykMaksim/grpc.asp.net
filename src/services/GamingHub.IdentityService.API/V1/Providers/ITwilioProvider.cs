namespace GamingHub.IdentityService.API.V1.Providers
{
    /// <summary>
    /// The interface allows to use Twilio's features.
    /// <see href="https://www.twilio.com/en-us"/>
    /// </summary>
    public interface ITwilioProvider
    {
        /// <summary>
        /// Sends an SMS code to specific number using Twilio Verify SDK.
        /// <see href="https://www.twilio.com/docs/verify/api#step-2-send-a-verification-token"/>
        /// </summary>
        /// <param name="serviceSid">
        /// The unique identifier of Twilio Verify service. 
        /// <seealso href="https://help.twilio.com/articles/14726256820123-What-is-a-Twilio-Account-SID-and-where-can-I-find-it-"/>
        /// </param>
        /// <param name="toPhoneNumber">The phone number to which the message is sent.</param>
        Task<bool> SendVerificationCodeAsync(
            string serviceSid,
            string toPhoneNumber);

        /// <summary>
        /// Checks the one-time password sent to the user by SMS using Twilio Verify SDK.
        /// The provided code is correct if the response 'status' parameter is 'approved'.
        /// <see href="https://www.twilio.com/docs/verify/api#step-3-check-the-verification-token"/>
        /// </summary>
        /// <param name="serviceSid">
        /// The unique identifier of Twilio Verify service. 
        /// <seealso href="https://help.twilio.com/articles/14726256820123-What-is-a-Twilio-Account-SID-and-where-can-I-find-it-"/>
        /// </param>
        /// <param name="toPhoneNumber">The phone number to which the message is sent.</param>
        /// <param name="code">The provided verification code.</param>
        /// <returns></returns>
        Task<bool> CheckVerificationCodeAsync(
            string serviceSid, 
            string toPhoneNumber,
            string code);

        /// <summary>
        /// Sends a message via SMS using Twilio Messaging SDK.
        /// <see href="https://www.twilio.com/docs/messaging/quickstart/csharp-dotnet-framework#send-an-sms-using-twilio-with-c"/>
        /// </summary>
        /// <param name="fromPhoneNumber">The phone number from which the message is sent.</param>
        /// <param name="toPhoneNumber">The phone number to which the message is sent.</param>
        /// <param name="msgBody">The message body.</param>
        /// <returns></returns>
        Task<bool> SendSmsAsync(
            string fromPhoneNumber, 
            string toPhoneNumber, 
            string msgBody);
    }
}
