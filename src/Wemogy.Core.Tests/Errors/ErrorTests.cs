using Wemogy.Core.Errors;
using Xunit;

namespace Wemogy.Core.Tests.Errors;

public class ErrorTests
{
    [Fact]
    public void CodeShouldBePrefixedCorrectly()
    {
        // Arrange
        var errorCode = "my_error_code";

        // Act
        var exception = Error.Conflict(errorCode, "description");

        // Assert
        Assert.Equal(errorCode, exception.Code);
    }
}
