using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wemogy.Core.Json.Converters
{
    public class DoubleTrimConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetDouble();

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
            => writer.WriteRawValue(value.ToString("G", CultureInfo.InvariantCulture));
    }
}
