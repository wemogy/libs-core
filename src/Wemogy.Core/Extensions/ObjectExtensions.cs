using System;
using System.Text.Json;
using Wemogy.Core.Json;

namespace Wemogy.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Clones a given object using JSON serialization and deserialization.
        /// Don't use this method for performance critical code.
        /// </summary>
        public static T Clone<T>(this T obj)
        {
            // Right now, this is the only way to clone an object.
            // Libraries like DeepClone, ObjectCloner, CloneExtensions, etc. are not supporting HashSet properties.
            // The hashSet.RemoveWhere() method is not working on the cloned object.
            var jsonDocument = JsonSerializer.SerializeToDocument(obj);
            return jsonDocument.Deserialize<T>()!;
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
