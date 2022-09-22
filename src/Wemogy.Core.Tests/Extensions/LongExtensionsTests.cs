using System.Globalization;
using System.Threading;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public class LongExtensionsTests
    {
        [Fact]
        public void HumanizeBytes_ShouldWork()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); // ensure that dots are used
            long bytes1 = 1200492;
            long bytes2 = 5083825232;
            long bytes3 = 58;
            long bytes4 = long.MaxValue;
            long bytes5 = 0;

            // Act
            var bytes1String = bytes1.HumanizeBytes();
            var bytes2String = bytes2.HumanizeBytes();
            var bytes3String = bytes3.HumanizeBytes();
            var bytes4String = bytes4.HumanizeBytes();
            var bytes5String = bytes5.HumanizeBytes();

            // Assert
            Assert.Equal("1.14 MB", bytes1String);
            Assert.Equal("4.73 GB", bytes2String);
            Assert.Equal("58 Bytes", bytes3String);
            Assert.Equal("8192 TB", bytes4String);
            Assert.Equal("0 Bytes", bytes5String);
        }
    }
}
