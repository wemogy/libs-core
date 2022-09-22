using Wemogy.Core.Primitives;
using Xunit;

namespace Wemogy.Core.Tests.Primitives
{
    enum TestPermissionFlags
    {
        Unknown = 0,
        CanRead = 1,
        CanWrite = 2,
        CanDelete = 3,
        CanComment = 4,
        CanSubscribe = 5,
        CanMove = 6
    }

    public class EnumBitsTests
    {
        [Fact]
        public void GenericBitsShouldWork()
        {
            // Arrange
            var enumBits = new EnumBits<TestPermissionFlags>("L"); // 001011

            // Act & Assert
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanRead));
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanWrite));
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanDelete));
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanComment));
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanSubscribe));
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanMove));

            Assert.True(enumBits.HasFlags(TestPermissionFlags.CanRead, TestPermissionFlags.CanWrite));
            Assert.False(enumBits.HasFlags(TestPermissionFlags.CanRead, TestPermissionFlags.CanWrite, TestPermissionFlags.CanDelete));
            Assert.True(enumBits.HasFlags());
        }

        [Fact]
        public void FromFlag_ShouldWorkForEmpty()
        {
            // Arrange
            var emptyEnumBits = EnumBits<TestPermissionFlags>.Empty;

            // Act
            var bits = EnumBits<TestPermissionFlags>.FromFlag(TestPermissionFlags.Unknown);

            // Assert
            Assert.Equal(emptyEnumBits, bits);
        }
    }
}
