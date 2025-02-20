using System.Security.Cryptography;

namespace GamingHub.Service.Shared.Utils
{
    /// <summary>
    /// Tool to generate probabilities.
    /// </summary>
    public static class ProbabilityGenerator
    {
        private static readonly Random Random = new();
        private static readonly RandomNumberGenerator SecureRandom = RandomNumberGenerator.Create();

        /// <summary>
        /// Generates random probability threshold and verifies
        /// if specified probability are includes generated threshold.
        /// </summary>
        /// <param name="probability"></param>
        /// <returns></returns>
        public static bool Verify(double probability)
        {
            var threshold = Random.NextDouble();

            return threshold < probability;
        }

        /// <summary>
        /// Generates random number in specified range.
        /// </summary>
        /// <remarks>This is a fast way to get a random number, but may be guessable.</remarks>
        /// <param name="min">The minimum value for the random number, inclusive.</param>
        /// <param name="max">The maximum value for the random number, exclusive.</param>
        /// <returns>A random number within the specified range.</returns>
        public static int GenerateRandomNumber(int min, int max)
        {
            return Random.Next(min, max);
        }

        /// <summary>
        /// Generates a random number in a specified range.
        /// </summary>
        /// <remarks>This is a slower way to get a random number, but it is much harder to guess.</remarks>
        /// <param name="minValue">The minimum value for the random number, inclusive.</param>
        /// <param name="maxValue">The maximum value for the random number, exclusive.</param>
        /// <returns>A random secure number within the specified range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the minValue is greater than the maxValue.</exception>
        public static int GenerateSecureRandomNumber(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));
            if (minValue == maxValue)
                return minValue;

            long diff = maxValue - minValue;
            const long max = (1 + (long)uint.MaxValue);
            var intByteArray = new byte[4];
            while (true)
            {
                SecureRandom.GetBytes(intByteArray);
                var rand = BitConverter.ToUInt32(intByteArray, 0);

                var remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (int)(minValue + (rand % diff));
                }
            }
        }

        /// <summary>
        /// Generates a random non-negative number less than <param name="maxValue"/>.
        /// </summary>
        /// <remarks>This is a slower way to get a random number, but it is much harder to guess.</remarks>
        /// <param name="maxValue">The maximum value for the random number, exclusive.</param>
        /// <returns>A random secure number within the specified range.</returns>
        public static int GenerateSecureRandomNumber(int maxValue)
        {
            return GenerateSecureRandomNumber(0, maxValue);
        }

        /// <summary>
        /// Generates random number in specified range.
        /// </summary>
        /// <param name="min">The minimum value for the random number, inclusive.</param>
        /// <param name="max">The maximum value for the random number, exclusive.</param>
        /// <returns>A random number within the specified range.</returns>
        public static long GenerateRandomNumber(long min, long max)
        {
            var buf = new byte[8];
            Random.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        /// <summary>
        /// The main function that returns a random item from values[] according to 
        /// distribution array defined by probes[].
        /// </summary>
        /// <typeparam name="T">The type of the values and the return type.</typeparam>
        /// <param name="values">The array of values corresponding probabilities.</param>
        /// <param name="probs">The array of probabilities.</param>
        /// <returns>A randomly selected value based off of the provided weights</returns>
        public static T Randomize<T>(T[] values, int[] probs)
        {
            return values[Randomize(probs)];
        }

        /// <summary>
        /// The main function that returns a random item from values[] according to 
        /// distribution array defined by probes[].
        /// </summary>
        /// <typeparam name="T">The type of the values and the return type.</typeparam>
        /// <param name="values">The array of values corresponding probabilities.</param>
        /// <param name="probs">The array of probabilities.</param>
        /// <returns>A randomly selected value based off of the provided weights</returns>
        public static T Randomize<T>(T[] values, decimal[] probs)
        {
            return values[Randomize(probs)];
        }

        /// <summary>
        /// Since our randomization algorithm only accepts integers but can accept probabilities that sum up to over 100,
        /// we need a way to determine the closest fit for a non-integer percentage scaled to 100. This method will give you
        /// how much to multiply the value by in order to get an integer.
        /// </summary>
        /// <remarks>
        /// We will cap the algorithm at a multiplier of 10,000,000 for safety reasons.
        /// Also, don't forget that the value is now out of 100 * the multiplier.
        /// </remarks>
        /// <param name="percentage">The percentage value scaled from 0-100, but with a potential fractional component.</param>
        /// <returns>The lowest multiplier to turn the double into an integer with no fractional component.</returns>
        public static int PercentageFitting(double percentage)
        {
            var multiplier = 1;

            for (var i = 0; i < 7 && percentage > Math.Truncate(percentage); i++)
            {
                percentage *= 10;
                multiplier *= 10;
            }

            return multiplier;
        }

        // The heart of the randomize methods. Given an array of probabilities which can be treated as weights,
        // an index into the array is chosen.
        private static int Randomize(int[] probs)
        {
            var probsLength = probs.Length;

            // Create and fill prefix array 
            var prefix = new int[probsLength];
            prefix[0] = probs[0];

            for (var i = 1; i < probsLength; ++i)
            {
                prefix[i] = prefix[i - 1] + probs[i];
            }

            // prefix[n-1] is sum of all frequencies. Generate a random number 
            // with value from 1 to this sum
            var randomValue = Random.Next();
            var r = (randomValue % prefix[probsLength - 1]) + 1;

            // Find index of ceiling of r in prefix array 
            var index = FindCeil(prefix, r, 0, probsLength - 1);

            return index;
        }

        // The heart of the randomize methods. Given an array of probabilities which can be treated as weights,
        // an index into the array is chosen.
        private static int Randomize(decimal[] probs)
        {
            var probsLength = probs.Length;

            // Create and fill prefix array 
            var prefix = new decimal[probsLength];
            prefix[0] = probs[0];

            for (var i = 1; i < probsLength; ++i)
            {
                prefix[i] = prefix[i - 1] + probs[i];
            }

            // prefix[n-1] is sum of all frequencies. Generate a random number 
            // with value from 1 to this sum 
            var randomValue = Random.Next();
            var remainder = randomValue % (prefix[probsLength - 1] * 100);
            var r = (remainder + 1) / 100;

            // Find index of ceiling of r in prefix array 
            var index = FindCeil(prefix, r, 0, probsLength - 1);

            return index;
        }

        // Utility function to find ceiling of r in values[l…h]
        private static int FindCeil(int[] values, int r, int l, int h)
        {
            int mid;

            while (l < h)
            {
                mid = l + ((h - l) >> 1); // Same as mid = (l+h)/2

                if (r > values[mid])
                {
                    l = mid + 1;
                }
                else
                {
                    h = mid;
                }
            }

            return (values[l] >= r) ? l : -1;
        }

        // Utility function to find ceiling of r in values[l..h]
        private static int FindCeil(decimal[] values, decimal r, int l, int h)
        {
            int mid;

            while (l < h)
            {
                mid = l + ((h - l) >> 1); // Same as mid = (l+h)/2

                if (r > values[mid])
                {
                    l = mid + 1;
                }
                else
                {
                    h = mid;
                }
            }

            return (values[l] >= r) ? l : -1;
        }
    }
}
