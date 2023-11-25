using System.Text.Json.Serialization;

namespace Wemogy.Core.Tests;

public class Demo
{
    [JsonInclude]
    public int Id { get; private set; }
}
