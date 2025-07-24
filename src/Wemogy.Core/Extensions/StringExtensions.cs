using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wemogy.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SecureSubstring(this string theString, int maxLength)
        {
            return theString.Length < maxLength ? theString : theString.Substring(0, maxLength);
        }

        public static string ReplaceSpecialCharacters(this string theString)
        {
            var map = new Dictionary<char, string>()
            {
                { 'ä', "ae" },
                { 'ö', "oe" },
                { 'ü', "ue" },
                { 'Ä', "Ae" },
                { 'Ö', "Oe" },
                { 'Ü', "Ue" },
                { 'ß', "ss" }
            };

            return theString.Aggregate(
                new StringBuilder(),
                (sb, c) => map.TryGetValue(c, out var r) ? sb.Append(r) : sb.Append(c))
            .ToString();
        }

        public static string RemoveWhitespaces(this string theString)
        {
            return theString.Replace(" ", string.Empty);
        }

        public static string ToCamelCase(this string theString)
        {
            return CaseExtensions.StringExtensions.ToCamelCase(theString);
        }

        public static string ToPascalCase(this string theString)
        {
            return CaseExtensions.StringExtensions.ToPascalCase(theString);
        }

        public static string CleanBeginAndEnd(this string theString)
        {
            // remove all line breaks at begin
            var result = theString.TrimStart(Environment.NewLine.ToCharArray());

            // remove all line breaks at end
            result = result.TrimEnd(Environment.NewLine.ToCharArray());

            // remove all whitespaces at begin and end
            result = result.Trim();

            return result;
        }

        /// <summary>
        /// Ensures that the returned string ends with a slash
        /// It adds a slash to the end if the original string had no slash at the end
        /// </summary>
        public static string WithSlashAtTheEnd(this string s)
        {
            if (s.EndsWith("/"))
            {
                return s;
            }

            return $"{s}/";
        }

        public static string WithSlashAtBegin(this string s)
        {
            if (s.StartsWith("/"))
            {
                return s;
            }

            return $"/{s}";
        }

        public static string WithoutSlashAtEnd(this string s)
        {
            if (!s.EndsWith("/"))
            {
                return s;
            }

            return s.Substring(0, s.Length - 1);
        }

        public static string WithoutSlashAtBegin(this string s)
        {
            if (!s.StartsWith("/"))
            {
                return s;
            }

            return s.Substring(1, s.Length - 1);
        }

        /// <summary>
        /// Because Path.Combine sometimes uses \\ instead of /, we have created our own
        /// </summary>
        public static string Combine(this string s, string sub)
        {
            return $"{s.WithoutSlashAtEnd()}/{sub.WithoutSlashAtBegin()}";
        }

        public static string ReplaceExtension(this string s, string newExtension)
        {
            var lastOccurrenceOfExtensionBeginIdentifier = s.LastIndexOf('.');

            // remove extension if one is defined
            if (lastOccurrenceOfExtensionBeginIdentifier != -1)
            {
                s = s.Substring(0, lastOccurrenceOfExtensionBeginIdentifier);
            }

            return $"{s}.{newExtension}";
        }

        public static bool IsBase64String(this string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
            {
                return false;
            }

            // Optional: Prüfe Länge (Base64-Längen sind immer vielfache von 4)
            if (base64.Length % 4 != 0)
            {
                return false;
            }

            try
            {
                _ = Convert.FromBase64String(base64);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string EncodeBase64String(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decodes a UTF8 encoded base64 string
        /// </summary>
        public static string DecodeBase64String(this string base64String)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }

        public static string[] SplitOnLastOccurrence(this string s, string splitCharacter)
        {
            var parts = s.Split(new[] { splitCharacter }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
            {
                return parts;
            }

            var firstPart = string.Empty;
            var firstParts = parts.Take(parts.Length - 1).ToList();
            for (int i = 0; i < firstParts.Count; i++)
            {
                if (i == 0)
                {
                    firstPart += firstParts[i];
                }
                else
                {
                    firstPart += $"{splitCharacter}{firstParts[i]}";
                }
            }

            return new[]
            {
                firstPart,
                parts.Last()
            };
        }

        public static string[] SplitOnFirstOccurrence(this string s, string splitCharacter)
        {
            var parts = s.Split(new[] { splitCharacter }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
            {
                return parts;
            }

            var lastPart = string.Empty;
            var lastParts = parts.Skip(1).ToList();
            for (int i = 0; i < lastParts.Count; i++)
            {
                if (i == 0)
                {
                    lastPart += lastParts[i];
                }
                else
                {
                    lastPart += $"{splitCharacter}{lastParts[i]}";
                }
            }

            return new[]
            {
                parts.First(),
                lastPart
            };
        }

        public static string RemoveFirstCharacter(this string s)
        {
            return s.Substring(1);
        }

        public static string RemoveLastCharacter(this string s)
        {
            return s.Remove(s.Length - 1);
        }

        public static string UpperFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// Converts all Diacritic characters in a string to their ASCII equivalent
        /// Courtesy: http://stackoverflow.com/a/13154805/476786
        /// A quick explanation:
        /// * Normalizing to form D splits charactes like è to an e and a nonspacing `
        /// * From this, the nospacing characters are removed
        /// * The result is normalized back to form C (I'm not sure if this is neccesary)
        /// </summary>
        public static string ConvertDiacriticToAscii(this string value)
        {
            var chars =
                value.Normalize(NormalizationForm.FormD)
                    .ToCharArray()
                    .Select(c => new { c, uc = CharUnicodeInfo.GetUnicodeCategory(c) })
                    .Where(@t => @t.uc != UnicodeCategory.NonSpacingMark)
                    .Select(@t => @t.c);
            var cleanStr = new string(chars.ToArray()).Normalize(NormalizationForm.FormC);
            return cleanStr;
        }

        public static string ReplaceLineBreaksForSendGrid(this string theString)
        {
            return theString.Replace("\n", "\r\n");
        }

        /// <summary>
        /// Splits a string on capitalLetters
        /// snaatchStorageSmallM => [snaatch, Storage, Small, M]
        /// Thanks to: https://stackoverflow.com/questions/4488969/split-a-string-by-capital-letters
        /// </summary>
        public static string[] SplitCamelCase(this string theString)
        {
            return Regex.Split(theString, @"(?<!^)(?=[A-Z])");
        }

        public static int CountOccurrences(this string theString, string substringToCount)
        {
            // Thanks to: https://stackoverflow.com/questions/541954/how-would-you-count-occurrences-of-a-string-actually-a-char-within-a-string?page=1&tab=votes#tab-top
            return theString.Length - theString.Replace(substringToCount, string.Empty).Length;
        }

        public static string RemoveLastPathPart(this string theString)
        {
            var indexOfLastSlash = theString.LastIndexOf('/');
            if (indexOfLastSlash < 0)
            {
                return theString;
            }

            return theString.Substring(0, indexOfLastSlash);
        }

        public static string RemoveRepeatingCharacters(this string theString, char charWhichShouldNotRepeated)
        {
            return Regex.Replace(theString, $"{charWhichShouldNotRepeated}+", "_");
        }

        public static Stream ToStream(this string theString, Encoding? encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var bytes = encoding.GetBytes(theString);
            return new MemoryStream(bytes);
        }

        /**
         * Removes all non alphanumeric characters from a string
         * Thanks to: https://stackoverflow.com/questions/3210393/how-do-i-remove-all-non-alphanumeric-characters-from-a-string-except-dash
         */
        public static string OnlyAlphanumeric(this string theString)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(theString, string.Empty);
        }

        public static IDictionary<string, string> GetConnectionStringAttributes(this string theString)
        {
            return theString.Split(';').Select(x => x.Split('=')).ToDictionary(x => x[0], x => x[1]);
        }

        public static string RemoveTrailingString(this string theString, string trailingString)
        {
            Regex rgx = new Regex($"{trailingString}$");
            return rgx.Replace(theString, string.Empty);
        }
    }
}
