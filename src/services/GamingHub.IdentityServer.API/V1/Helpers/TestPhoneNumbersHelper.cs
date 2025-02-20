namespace GamingHub.IdentityServer.API.V1.Helpers
{
    /// <summary>
    /// Class manages test phone numbers
    /// </summary>
    public static class TestPhoneNumbersHelper
    {
        /// <summary>
        /// Test phone numbers for test purpose only.
        /// </summary>
        public static IList<string> PhoneNumbers { get; set; } = new List<string>
        {
            "+15005550000",
        };

        /// <summary>
        /// Valid 6-digit codes from SMS for test purpose only.
        /// </summary>
        public static IList<string> ValidCodes { get; set; } = new List<string>
        {
            "000001",
        };
    }
}