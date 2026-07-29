using System;
using System.Security.Cryptography;

namespace Wemogy.Core.Helpers
{
    public class RandomGenerator
    {
        public static string GenerateRandomToken(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_")
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(length, 0, nameof(length));
            ArgumentException.ThrowIfNullOrWhiteSpace(chars, nameof(chars));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(chars.Length, byte.MaxValue + 1, nameof(chars));

            // Maximum random byte value usable without introducing modulo bias
            int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);

            Span<byte> data = length <= 256 ? stackalloc byte[length] : new byte[length];
            RandomNumberGenerator.Fill(data);

            Span<char> result = length <= 256 ? stackalloc char[length] : new char[length];
            Span<byte> retryBuffer = stackalloc byte[1];

            for (int i = 0; i < length; i++)
            {
                byte value = data[i];
                while (value > maxRandom)
                {
                    // If chars.Length isn't a power of 2, using modulus directly would bias
                    // the first characters of chars over the last ones. Rejection sampling
                    // (regenerating out-of-range bytes) avoids that bias.
                    RandomNumberGenerator.Fill(retryBuffer);
                    value = retryBuffer[0];
                }

                result[i] = chars[value % chars.Length];
            }

            return new string(result);
        }

        [Obsolete("Use GenerateRandomToken instead. This method does not guarantee uniqueness.")]
        public static string GetUniqueToken(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_")
        {
            return GenerateRandomToken(length, chars);
        }

        public static string GenerateRandomPassword(int length)
        {
            return GenerateRandomToken(length);
        }
    }
}
