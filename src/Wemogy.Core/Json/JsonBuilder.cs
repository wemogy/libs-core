using System.Collections.Generic;
using Wemogy.Core.Extensions;

namespace Wemogy.Core.Json
{
    public class JsonBuilder
    {
        private readonly Dictionary<string, string> _properties;

        public JsonBuilder()
        {
            _properties = new Dictionary<string, string>();
        }

        public JsonBuilder AddProperty(string key, object? value)
        {
            if (value != null)
            {
                _properties.Add(key, value.ToJson());
            }

            return this;
        }

        public JsonBuilder AddJsonObjectProperty(string key, string value)
        {
            _properties.Add(key, value);
            return this;
        }

        public JsonBuilder AddProperty(string key, string? value)
        {
            if (value != null)
            {
                _properties.Add(key, $"\"{value}\"");
            }

            return this;
        }

        public string Build()
        {
            var jsonString = "{";

            foreach (var property in _properties)
            {
                jsonString += $"\"{property.Key}\": {property.Value},";
            }

            jsonString = jsonString.TrimEnd(',');
            jsonString += "}";
            return jsonString;
        }

        public T Build<T>()
        {
            var jsonString = Build();
            return jsonString.FromJson<T>() !;
        }
    }
}
