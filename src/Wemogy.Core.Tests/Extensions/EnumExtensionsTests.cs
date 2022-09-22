using System;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    [Flags]
    enum IntEnum
    {
        OptionA = 1 << 3
    }

    [Flags]
    enum LongEnum : long
    {
        OptionA = 1L << 35
    }

    public class EnumExtensionsTests
    {
        [Fact]
        public void ToInt_ShouldWork()
        {
            // Arrange
            var enumValue = IntEnum.OptionA;

            // Act
            var enumValueAsInt = enumValue.ToInt();

            // Assert
            Assert.Equal(1 << 3, enumValueAsInt);
        }

        [Fact]
        public void ToLong_ShouldWork_ForIntEnums()
        {
            // Arrange
            var enumValue = IntEnum.OptionA;

            // Act
            var enumValueAsInt = enumValue.ToLong();

            // Assert
            Assert.Equal(1L << 3, enumValueAsInt);
        }

        [Fact]
        public void ToLong_ShouldWork()
        {
            // Arrange
            var enumValue = LongEnum.OptionA;

            // Act
            var enumValueAsLong = enumValue.ToLong();

            // Assert
            Assert.Equal(1L << 35, enumValueAsLong);
        }
    }
}
