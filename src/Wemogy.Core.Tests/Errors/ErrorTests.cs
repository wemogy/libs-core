using Wemogy.Core.Errors;
using Xunit;

namespace Wemogy.Core.Tests.Errors;

public class ErrorTests
{
    [Fact]
    public void CodeShouldBePrefixedCorrectly()
    {
        // Arrange

        // Act
        var exception = Error.Conflict("test", "description");

        // Assert
        Assert.Equal("ErrorTests-CodeShouldBePrefixedCorrectly-test", exception.Code);
    }
}
