using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public class IntExtensionsTests
    {
        [Fact]
        public void ToBinaryString_ShouldWork()
        {
            // Arrange
            var decimal1 = 0;
            var decimal2 = 63;
            var decimal3 = 256;
            var decimal4 = 46;
            var padMultiple = 6;

            // Act
            var decimal1BinaryString = decimal1.ToBinaryString();
            var decimal2BinaryString = decimal2.ToBinaryString();
            var decimal3BinaryString = decimal3.ToBinaryString();
            var decimal4BinaryString = decimal4.ToBinaryString();
            var decimal1BinaryStringPadded = decimal1.ToBinaryString(padMultiple);
            var decimal2BinaryStringPadded = decimal2.ToBinaryString(padMultiple);
            var decimal3BinaryStringPadded = decimal3.ToBinaryString(padMultiple);
            var decimal4BinaryStringPadded = decimal4.ToBinaryString(padMultiple);

            // Assert
            Assert.Equal("0", decimal1BinaryString);
            Assert.Equal("111111", decimal2BinaryString);
            Assert.Equal("100000000", decimal3BinaryString);
            Assert.Equal("101110", decimal4BinaryString);
            Assert.Equal("000000", decimal1BinaryStringPadded);
            Assert.Equal("111111", decimal2BinaryStringPadded);
            Assert.Equal("000100000000", decimal3BinaryStringPadded);
            Assert.Equal("101110", decimal4BinaryStringPadded);
        }
    }
}
