using System;
using System.Text.Json;
using Wemogy.Core.Json;
using Xunit;

namespace Wemogy.Core.Tests;

public class WemogyJsonTests
{
    [Fact]
    public void PrivateSetters_GetSet()
    {
        // Arrange
        var json = @"{""id"": 123}";

        // Act
        var demo = JsonSerializer.Deserialize<Demo>(json, WemogyJson.Options);

        // Assert
        Assert.Equal(123, demo!.Id);
    }
}
