using System;
using System.Linq;
using Wemogy.Core.Encodings;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Encodings
{
    public class Base64Tests
    {
        [Fact]
        public void EncodeUrl_ShouldWork()
        {
            // Arrange
            var bits1 = new[] { true, false, true, false, false, true }; // 100101 (l)
            var bits2 = Array.Empty<bool>(); // 000000
            var bits3 = new[] { false, false, false, false, false, false }; // 000000
            var bits4 = new[]
                {
                    false, false, false, false, false, false, false, false, false, false, false, false
                }; // 000000 000000
            var bits5 = new[]
                {
                    false, false, true, true, false, true, true, false, true, false, false, true
                }; // 100101(l) 101100(s)
            var bits6 = new[] { true, true, false, true, true, false, true, false, false, true }; // 1001(J) 011011(b)

            // Act
            var bits1Base64UrlEncoded = Base64.EncodeUrl(bits1.ToList());
            var bits2Base64UrlEncoded = Base64.EncodeUrl(bits2.ToList());
            var bits3Base64UrlEncoded = Base64.EncodeUrl(bits3.ToList());
            var bits4Base64UrlEncoded = Base64.EncodeUrl(bits4.ToList());
            var bits5Base64UrlEncoded = Base64.EncodeUrl(bits5.ToList());
            var bits6Base64UrlEncoded = Base64.EncodeUrl(bits6.ToList());

            // Assert
            Assert.Equal("l", bits1Base64UrlEncoded);
            Assert.Equal("A", bits2Base64UrlEncoded);
            Assert.Equal("A", bits3Base64UrlEncoded);
            Assert.Equal("AA", bits4Base64UrlEncoded);
            Assert.Equal("ls", bits5Base64UrlEncoded);
            Assert.Equal("Jb", bits6Base64UrlEncoded);
        }

        [Fact]
        public void DecodeUrl_ShouldWork()
        {
            // Arrange
            var base64UrlString1 = "Az0_";

            // Act
            var base64UrlString1Decoded = Base64.DecodeUrl(base64UrlString1);

            // Assert
            // 000000(A) 110011(z) 110100(0) 111111(_)
            Assert.Equal("000000110011110100111111", base64UrlString1Decoded.ToBitString());
            Assert.True(base64UrlString1Decoded[0]);
            Assert.True(base64UrlString1Decoded[5]);
            Assert.False(base64UrlString1Decoded[6]);
            Assert.True(base64UrlString1Decoded[8]);
        }

        [Fact]
        public void DecodeUrl_ShouldThrowIfInvalidString()
        {
            // Arrange
            var invalidBase64UrlString = "Az0_+/";

            // Act & Assert
            Assert.Throws<Exception>(() => Base64.DecodeUrl(invalidBase64UrlString));
        }
    }
}
