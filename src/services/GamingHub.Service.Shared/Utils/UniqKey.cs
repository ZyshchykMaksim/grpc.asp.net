using System.Security.Cryptography;
using System.Text;

namespace GamingHub.Service.Shared.Utils
{
    /// <summary>
    /// The class allows to generate a unique key.
    /// </summary>
    public static class UniqKey
    {
        private const string LowerCase = "abcdefghijklmnopqursuvwxyz";
        private const string UpperCaes = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numbers = "123456789";
        private const string Specials = @"_$!=";

        private static readonly char[] _alphabeth = (UpperCaes + Numbers).ToCharArray();

        /// <summary>
        /// Generates a unique key.
        /// </summary>
        /// <param name="size">The number of generated characters in a unique key.</param>
        /// <returns></returns>
        public static string Generate(int size)
        {
            var data = new byte[1];
            var crypto = RandomNumberGenerator.Create();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(_alphabeth[b % (_alphabeth.Length - 1)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Generates a unique key.
        /// </summary>
        /// <param name="size">The number of generated characters in a unique key.</param>
        /// <param name="useLowercase">The flag indicates that the key will contain lowercase characters.</param>
        /// <param name="useUppercase">The flag indicates that the key will contain uppercase characters.</param>
        /// <param name="useNumbers">The flag indicates that the key will contain numbers.</param>
        /// <param name="useSpecial">The flag indicates that the key will special characters.</param>
        /// <returns></returns>
        public static string GenerateEx(
            int size, 
            bool useLowercase = true, 
            bool useUppercase = true, 
            bool useNumbers = true, 
            bool useSpecial = true)
        {
            var charSet = "";
            // Build up the character set to choose from
            if (useLowercase) charSet += LowerCase;
            if (useUppercase) charSet += UpperCaes;
            if (useNumbers) charSet += Numbers;
            if (useSpecial) charSet += Specials;
            var alphabeth = charSet.ToCharArray();

            var data = new byte[1];
            var crypto = RandomNumberGenerator.Create();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(alphabeth[b % (alphabeth.Length - 1)]);
            }

            return result.ToString();
        }

    }
}
