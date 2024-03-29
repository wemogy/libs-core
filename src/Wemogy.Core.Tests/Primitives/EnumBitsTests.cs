using Wemogy.Core.Primitives;
using Wemogy.Core.Tests.Enums;
using Xunit;

namespace Wemogy.Core.Tests.Primitives
{
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

        [Fact]
        public void SetFlag_ShouldWork()
        {
            // Arrange
            var enumBits = EnumBits<TestPermissionFlags>.Empty;

            // Act
            enumBits.SetFlag(TestPermissionFlags.CanMove);

            // Assert
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanMove));
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanComment));
        }

        [Fact]
        public void SetFlags_ShouldWork()
        {
            // Arrange
            var enumBits = EnumBits<TestPermissionFlags>.Empty;

            // Act
            enumBits.SetFlags(TestPermissionFlags.CanMove, TestPermissionFlags.CanComment);

            // Assert
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanMove));
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanComment));
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanRead));
        }

        [Fact]
        public void RemoveFlag_ShouldWork()
        {
            // Arrange
            var enumBits = new EnumBits<TestPermissionFlags>("L"); // 001011

            // Act
            enumBits.RemoveFlag(TestPermissionFlags.CanMove);

            // Assert
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanMove));
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanComment));
        }

        [Fact]
        public void RemoveFlags_ShouldWork()
        {
            // Arrange
            var enumBits = new EnumBits<TestPermissionFlags>("L"); // 001011

            // Act
            enumBits.RemoveFlags(TestPermissionFlags.CanMove, TestPermissionFlags.CanComment);

            // Assert
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanMove));
            Assert.False(enumBits.HasFlag(TestPermissionFlags.CanComment));
            Assert.True(enumBits.HasFlag(TestPermissionFlags.CanRead));
        }

        [Fact]
        public void Or_ShouldWork()
        {
            // Arrange
            var enumBits1 = new EnumBits<TestPermissionFlags>("L"); // 001011
            var enumBits2 = new EnumBits<TestPermissionFlags>("M"); // 001100

            // Act
            var orResult = enumBits1.Or(enumBits2);

            // Assert
            Assert.Equal("P", orResult.ToString()); // 001111
        }

        [Fact]
        public void Or_ShouldWorkForEmpty()
        {
            // Arrange
            var enumBits1 = new EnumBits<TestPermissionFlags>("L"); // 001011
            var enumBits2 = EnumBits<TestPermissionFlags>.Empty;

            // Act
            var orResult = enumBits1.Or(enumBits2);

            // Assert
            Assert.Equal("L", orResult.ToString()); // 001011
        }

        [Fact]
        public void And_ShouldWork()
        {
            // Arrange
            var enumBits1 = new EnumBits<TestPermissionFlags>("L"); // 001011
            var enumBits2 = new EnumBits<TestPermissionFlags>("M"); // 001100

            // Act
            var andResult = enumBits1.And(enumBits2);

            // Assert
            Assert.Equal("I", andResult.ToString()); // 001000
        }

        [Fact]
        public void And_ShouldWorkForEmpty()
        {
            // Arrange
            var enumBits1 = new EnumBits<TestPermissionFlags>("L"); // 001011
            var enumBits2 = EnumBits<TestPermissionFlags>.Empty;

            // Act
            var andResult = enumBits1.And(enumBits2);

            // Assert
            Assert.Equal("A", andResult.ToString()); // 000000
        }
    }
}
