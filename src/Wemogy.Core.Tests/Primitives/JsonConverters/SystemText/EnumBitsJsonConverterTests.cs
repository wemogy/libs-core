using System.Collections.Generic;
using System.Text.Json;
using Wemogy.Core.Primitives;
using Wemogy.Core.Tests.Enums;
using Xunit;

// ReSharper disable CollectionNeverQueried.Global

namespace Wemogy.Core.Tests.Primitives.JsonConverters.SystemText;

public class EnumBitsJsonConverterTests
{
    [Fact]
    public void EnumBitsJsonConverter_Write_ShouldWork()
    {
        // Arrange
        var bitsBase64UrlValue = "Azb_-92";
        var bits = new Bits(bitsBase64UrlValue);
        var modelWithBitsProps = new ModelWithEnumBitsProps()
        {
            Bits = EnumBits<TestPermissionFlags>.Empty,
            BitsNull = null,
            BitsList = new List<EnumBits<TestPermissionFlags>?>()
            {
                EnumBits<TestPermissionFlags>.Empty,
                null,
                EnumBits<TestPermissionFlags>.Empty
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
    public void EnumBitsJsonConverter_Read_ShouldWork()
    {
        // Arrange
        var bits = new Bits();
        var modelWithBitsProps = new ModelWithEnumBitsProps()
        {
            Bits = EnumBits<TestPermissionFlags>.Empty,
            BitsNull = null,
            BitsList = new List<EnumBits<TestPermissionFlags>?>()
            {
                EnumBits<TestPermissionFlags>.Empty,
                null,
                EnumBits<TestPermissionFlags>.Empty
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

class ModelWithEnumBitsProps
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public EnumBits<TestPermissionFlags> Bits { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public EnumBits<TestPermissionFlags>? BitsNull { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<EnumBits<TestPermissionFlags>?> BitsList { get; set; }

    public ModelWithEnumBitsProps()
    {
        Bits = EnumBits<TestPermissionFlags>.Empty;
        BitsNull = null;
        BitsList = new List<EnumBits<TestPermissionFlags>?>();
    }
}
