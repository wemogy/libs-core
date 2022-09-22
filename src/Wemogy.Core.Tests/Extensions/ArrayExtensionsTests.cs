using System.Collections.Generic;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions;

public class ArrayExtensionsTests
{
    [Fact]
    public void TakeAsArray_ShouldWork()
    {
        // Arrange
        var numbers = new List<int>()
        {
            2, 8, 10
        };

        // Act
        var firstTwoAsArray = numbers.TakeAsArray(2);

        // Assert
        Assert.Equal(2, firstTwoAsArray.Length);
        Assert.IsType<int[]>(firstTwoAsArray);
    }
}
