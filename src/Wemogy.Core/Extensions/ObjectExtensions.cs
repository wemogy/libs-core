using System;
using System.Text.Json;
using Wemogy.Core.Json;

namespace Wemogy.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T obj)
            where T : class
        {
            var clone = ObjectCloner.ObjectCloner.DeepClone(obj);
            return clone;
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonSerializer.Serialize(obj, WemogyJson.Options);
        }

        public static JsonDocument ToJsonDocument<T>(this T obj)
        {
            return JsonSerializer.SerializeToDocument(obj, WemogyJson.Options);
        }

        public static object? FromJson(this string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, WemogyJson.Options);
        }

        public static T? FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, WemogyJson.Options);
        }

        public static T? FromJsonDocument<T>(this JsonDocument json)
        {
            return json.Deserialize<T>(WemogyJson.Options);
        }
    }
}
