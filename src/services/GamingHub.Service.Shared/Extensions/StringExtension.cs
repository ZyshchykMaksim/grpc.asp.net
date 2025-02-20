namespace GamingHub.Service.Shared.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Format phone number. 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string FormatPhoneNumber(this string phoneNumber)
        {
            var charsToRemove = new[] { " ", "(", ")", "-" };
            phoneNumber = charsToRemove.Aggregate(phoneNumber, (current, c) => current.Replace(c, string.Empty));

            if (phoneNumber[0] != '+')
            {
                phoneNumber = $"+{phoneNumber}";
            }

            return phoneNumber;
        }
    }
}
