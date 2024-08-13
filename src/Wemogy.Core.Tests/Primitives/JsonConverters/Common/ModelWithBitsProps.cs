using System.Collections.Generic;
using Wemogy.Core.Primitives;

namespace Wemogy.Core.Tests.Primitives.JsonConverters.Common;

public class ModelWithBitsProps
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
