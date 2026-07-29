using System.Collections.Generic;
using System.Text.Json;
using Wemogy.Core.Primitives;
using Wemogy.Core.Tests.Primitives.JsonConverters.Common;
using Xunit;

// ReSharper disable CollectionNeverQueried.Global

namespace Wemogy.Core.Tests.Primitives.JsonConverters.SystemText;

public class BitsJsonConverterTests
{
    [Fact]
    public void BitsJsonConverter_Write_ShouldWork()
    {
        // Arrange
        var bitsBase64UrlValue = "Azb_-92";
        var bits = new Bits(bitsBase64UrlValue);
        var modelWithBitsProps = new ModelWithBitsProps()
        {
            Bits = new Bits(),
            BitsNull = null,
            BitsList = new List<Bits?>()
            {
                new Bits(),
                null,
                new Bits()
            }
        };

        // Act
        var bitsJson = JsonSerializer.Serialize(bits);
        var modelWithBitsPropsJson = JsonSerializer.Serialize(modelWithBitsProps);

        // Assert
        Assert.NotEmpty(bitsJson);
        Assert.NotEmpty(modelWithBitsPropsJson);
    }

    [Fact]
    public void BitsJsonConverter_Read_ShouldWork()
    {
        // Arrange
        var bits = new Bits();
        var modelWithBitsProps = new ModelWithBitsProps()
        {
            Bits = new Bits(),
            BitsNull = null,
            BitsList = new List<Bits?>()
            {
                new Bits(),
                null,
                new Bits()
            }
        };
        var bitsJson = JsonSerializer.Serialize(bits);
        var modelWithBitsPropsJson = JsonSerializer.Serialize(modelWithBitsProps);

        // Act
        var bitsDeserialized = JsonSerializer.Deserialize<Bits>(bitsJson);
        var modelWithBitsPropsDeserialized = JsonSerializer.Deserialize<ModelWithBitsProps>(modelWithBitsPropsJson);

        // Assert
        Assert.NotNull(bitsDeserialized);
        Assert.NotNull(modelWithBitsPropsDeserialized);
    }
}
