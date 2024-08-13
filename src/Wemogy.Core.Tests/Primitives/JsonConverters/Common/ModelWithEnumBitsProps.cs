using System.Collections.Generic;
using Wemogy.Core.Primitives;
using Wemogy.Core.Tests.Enums;

namespace Wemogy.Core.Tests.Primitives.JsonConverters.Common;

public class ModelWithEnumBitsProps
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
