using System;
using System.Collections.Generic;
using System.Linq;
using Wemogy.Core.Extensions;

namespace Wemogy.Core.Encodings
{
    public static class Base64
    {
        private static readonly char[] Base64UrlCharset =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
        };

        public static string EncodeUrl(List<bool> bits)
        {
            if (!bits.Any())
            {
                return Base64UrlCharset[0].ToString();
            }

            // copy the list
            bits = bits.Select(x => x).ToList();

            // ensure the bits length is a multiple of 6
            bits.Pad(6);

            // map 6 items to the base 64 representation of them
            var base64UrlChars = bits.MapChunks(6, BitChunkToBase64Url);

            // join the base64 chars to a string
            base64UrlChars.Reverse();
            var base64UrlString = new string(base64UrlChars.ToArray());

            return base64UrlString;
        }

        public static List<bool> DecodeUrl(string base64Url)
        {
            // split into base64Url characters
            var base64UrlChars = base64Url.ToCharArray();

            // map character to bit chunk
            var bitChunks = base64UrlChars.Reverse().Select(Base64UrlCharToBitChunk).ToList();

            // merge the chunks
            var bits = bitChunks.SelectMany(x => x).ToList();

            // return the bits
            return bits;
        }

        private static char BitChunkToBase64Url(List<bool> bits)
        {
            var bitsString = bits.ToBitString();

            // convert binary value to decimal value
            var bitsValue = Convert.ToInt32(bitsString, 2);

            // access the character of the decimal value
            return Base64UrlCharset[bitsValue];
        }

        private static List<bool> Base64UrlCharToBitChunk(char base64UrlChar)
        {
            // map the character to the decimal value
            var decimalValue = Base64UrlCharset.ToList().IndexOf(base64UrlChar);

            // throw if the char is not part of charset
            if (decimalValue == -1)
            {
                throw new Exception($"the char {base64UrlChar} is not a valid base64url char");
            }

            // get binary string
            var binary = decimalValue.ToBinaryString(6);

            // convert to bit chunk
            var bitChunk = binary.ToCharArray().Reverse();
            var bitChunkBits = bitChunk.Select(bit => bit == '1').ToList();

            // return the chunk
            return bitChunkBits;
        }
    }
}
