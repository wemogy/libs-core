using System.Collections.Generic;
using Wemogy.Core.Extensions;
using Wemogy.Core.Primitives;
using Xunit;

namespace Wemogy.Core.Tests.Primitives.JsonConverters.Newtonsoft;

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
        var bitsJson = bits.ToJson();
        var modelWithBitsPropsJson = modelWithBitsProps.ToJson();

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
        var bitsJson = bits.ToJson();
        var modelWithBitsPropsJson = modelWithBitsProps.ToJson();

        // Act
        var bitsDeserialized = bitsJson.FromJson<Bits>();
        var exception = Record.Exception(() => modelWithBitsPropsJson.FromJson<ModelWithBitsProps>());

        // Assert
        Assert.Null(exception);
        Assert.NotNull(bitsDeserialized);
        Assert.NotNull(modelWithBitsPropsJson);
    }
}

class ModelWithBitsProps
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Bits Bits { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Bits? BitsNull { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<Bits?> BitsList { get; set; }

    public ModelWithBitsProps()
    {
        Bits = new Bits();
        BitsNull = null;
        BitsList = new List<Bits?>();
    }
}
