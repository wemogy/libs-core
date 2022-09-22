using Wemogy.Core.Primitives;
using Xunit;

namespace Wemogy.Core.Tests.Primitives
{
    public class BitsTests
    {
        [Fact]
        public void Constructor_Initialize()
        {
            // Arrange
            var base64UrlEncoded = "L"; // 001011

            // Act
            var bits = new Bits(base64UrlEncoded);

            // Assert
            Assert.Equal(base64UrlEncoded, bits.ToString());
        }

        [Fact]
        public void Constructor_Initialize_ShouldWork_WithoutValue()
        {
            // Arrange
            var base64UrlEncoded = "A"; // 000000

            // Act
            var bits = new Bits();

            // Assert
            Assert.Equal(base64UrlEncoded, bits.ToString());
        }

        [Fact]
        public void Equals_ShouldWork()
        {
            // Arrange
            var bit1 = new Bits("Abs");
            var bit2 = new Bits("Abs");
            var bit3 = new Bits("AAAAbs");
            var bit4 = new Bits("Abs");

            // Act & Assert
            Assert.True(bit1.Equals(bit2));
            Assert.True(bit3.Equals(bit4));
        }

        [Theory]
        [InlineData("A", "31", "AA")]
        [InlineData("31", "31", "31")]
        [InlineData("*", "31", "31")]
        [InlineData("31", "*", "31")]
        public void AndOperation(string bits1Base64, string bits2Base64, string expectedBits)
        {
            // Arrange
            var bits1 = new Bits(bits1Base64);
            var bits2 = new Bits(bits2Base64);

            // Act
            var result = bits1.And(bits2);

            // Assert
            Assert.Equal(expectedBits, result.ToString());
        }

        [Theory]
        [InlineData("A", "31", "31")]
        [InlineData("31", "31", "31")]
        [InlineData("*", "31", "*")]
        [InlineData("31", "*", "*")]
        public void OrOperation(string bits1Base64, string bits2Base64, string expectedBits)
        {
            // Arrange
            var bits1 = new Bits(bits1Base64);
            var bits2 = new Bits(bits2Base64);

            // Act
            var result = bits1.Or(bits2);

            // Assert
            Assert.Equal(expectedBits, result.ToString());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(22)]
        [InlineData(1000)]
        public void HasFlag_WorksForWildcard(int expectedFlagIndex)
        {
            // Arrange
            var bits = new Bits("*");

            // Act
            var bitsHasFlag = bits.HasFlag(expectedFlagIndex);

            // Assert
            Assert.True(bitsHasFlag);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(22)]
        [InlineData(1000)]
        public void RemoveFlag_WorksForWildcard(int flagToRemove)
        {
            // Arrange
            var bits = new Bits("*");

            // Act
            var exception = Record.Exception(() => bits.RemoveFlag(flagToRemove));

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(22)]
        [InlineData(1000)]
        public void SetFlag_WorksForWildcard(int flagToSet)
        {
            // Arrange
            var bits = new Bits("*");

            // Act
            var exception = Record.Exception(() => bits.SetFlag(flagToSet));

            // Assert
            Assert.Null(exception);
        }
    }
}
