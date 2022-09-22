using System.Linq;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void RemoveRepeatingCharacters_ShouldWork()
        {
            var theString = "ab__cd___e_f";

            var cleanString = theString.RemoveRepeatingCharacters('_');

            Assert.Equal("ab_cd_e_f", cleanString);
        }

        [Fact]
        public void SplitOnFirstOccurrence_ShouldWork()
        {
            var theString = "Select * FROM root WHERE FROM root IS";

            var last = theString.SplitOnFirstOccurrence("FROM root");

            Assert.Equal(" WHERE FROM root IS", last.Last());
        }

        [Fact]
        public void SplitOnLastOccurrence_ShouldWork()
        {
            var theString = "Select * FROM root WHERE FROM root IS";

            var last = theString.SplitOnLastOccurrence("FROM root");

            Assert.Equal(" IS", last.Last());
        }

        [Fact]
        public void RemoveFirstCharacter_ShouldWork()
        {
            // Arrange
            var theString = "Hello World";

            // Act
            var withoutFirstCharacter = theString.RemoveFirstCharacter();

            // Assert
            Assert.Equal("ello World", withoutFirstCharacter);
        }

        [Fact]
        public void RemoveLastCharacter_ShouldWork()
        {
            // Arrange
            var theString = "Hello World";

            // Act
            var withoutFirstCharacter = theString.RemoveLastCharacter();

            // Assert
            Assert.Equal("Hello Worl", withoutFirstCharacter);
        }

        [Fact]
        public void RemoveTrailingString_ShouldWork()
        {
            var theString = "ab_cd__e_f_";

            var withoutTrailing = theString.RemoveTrailingString("_");

            Assert.Equal("ab_cd__e_f", withoutTrailing);
        }
    }
}
