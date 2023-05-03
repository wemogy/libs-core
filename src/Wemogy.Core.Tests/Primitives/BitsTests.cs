using Wemogy.Core.Primitives;
using Wemogy.Core.Tests.Enums;
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
            var bits1 = new Bits("Abs");
            var bits2 = new Bits("Abs");
            var bits3 = new Bits("AAAAbs");
            var bits4 = new Bits("Abs");

            // Act & Assert
            Assert.True(bits1.Equals(bits2));
            Assert.True(bits3.Equals(bits4));
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

        [Fact]
        public void Empty_ShouldWork()
        {
            // Arrange
            var bits1 = Bits.Empty;
            var bits2 = new Bits();

            // Act & Assert
            Assert.True(bits1.Equals(bits2));
        }

        [Fact]
        public void Wildcard_ShouldWork()
        {
            // Arrange
            var bits1 = Bits.Wildcard;
            var bits2 = new Bits("*");

            // Act & Assert
            Assert.True(bits1.Equals(bits2));
        }

        [Theory]
        [InlineData("D", TestEnum.None, TestEnum.Value1, TestEnum.Value2)]
        [InlineData("F", TestEnum.Value3, TestEnum.Value1, TestEnum.None)]
        [InlineData("H", TestEnum.Value1, TestEnum.Value3, TestEnum.Value2)]
        public void FromFlags_ShouldWork(string expectedBits, TestEnum enum1, TestEnum enum2, TestEnum enum3)
        {
            // Arrange & Act
            var result = Bits.FromFlags(enum1, enum2, enum3);

            // Assert
            Assert.Equal(expectedBits, result.ToString());
        }

        [Fact]
        public void FromFlags_WorksWithNoParam()
        {
            // Arrange
            var expectedBits = new Bits().ToString();

            // Act
            var result = Bits.FromFlags();

            // Assert
            Assert.Equal(expectedBits, result.ToString());
        }

        [Theory]
        [InlineData("*", "111111")]
        [InlineData("*", "111", 3)]
        [InlineData("*", "11111111111", 11)]
        [InlineData("A", "000000")]
        [InlineData("A", "000000000000", 12)]
        [InlineData("F", "101000")]
        [InlineData("F", "101000000000", 12)]
        [InlineData("HDF", "101000110000111000")]
        [InlineData("HDF", "10100011000011100000", 20)]
        public void ToBinaryString_ShouldWork(string bitsBase64, string expectedBinaryString, int? length = null)
        {
            // Arrange
            var bits = new Bits(bitsBase64);

            // Act
            var binaryString = bits.ToBinaryString(length);

            // Assert
            Assert.Equal(expectedBinaryString, binaryString);
        }
    }
}
