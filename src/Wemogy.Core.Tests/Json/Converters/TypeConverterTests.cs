using System;
using System.Text.Json;
using FluentAssertions;
using Wemogy.Core.Errors;
using Wemogy.Core.Json;
using Xunit;

namespace Wemogy.Core.Tests.Json.Converters;

public class TypeConverterTests
{
    [Theory]
    [InlineData(typeof(string))]
    [InlineData(typeof(int))]
    [InlineData(typeof(TypeConverterTests))]
    [InlineData(typeof(Error))]
    [InlineData(typeof(FluentAssertions.FluentActions))]
    public void TypeConverter_ShouldConvertAType(Type type)
    {
        // Arrange

        // Act
        var json = JsonSerializer.Serialize(type, WemogyJson.Options);
        var result = JsonSerializer.Deserialize<Type>(json, WemogyJson.Options);

        // Assert
        result.Should().Be(type);
    }
}
