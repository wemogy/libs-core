using System.Collections.Generic;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions;

public class DictionaryExtensionsTests
{
    [Fact]
    public void PutShouldWork()
    {
        // Arrange
        var key = "k";
        var dictionary = new Dictionary<string, string>();

        // Act
        dictionary.Put(key, "v1");
        var valueAfterFirstPut = dictionary[key];
        dictionary.Put(key, "v2");
        var valueAfterSecondPut = dictionary[key];

        // Assert
        Assert.Equal("v1", valueAfterFirstPut);
        Assert.Equal("v2", valueAfterSecondPut);
    }
}
