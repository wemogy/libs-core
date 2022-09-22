using System;
using System.Collections.Generic;

namespace Wemogy.Core.Tests.Jwt.Models;

public class SimpleJwtPayload
{
    public string? Sub { get; set; }

    public string? Aud { get; set; }

    public DateTime Exp { get; set; }

    public List<string> Scp { get; set; }

    public ExtendedJwtPayload Ext { get; set; }

    public SimpleJwtPayload()
    {
        Ext = new ExtendedJwtPayload();
        Scp = new List<string>();
    }
}
