using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wemogy.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetPropertyNameFromPath(string path)
        {
            // Path looks like this: /propA/propB/1
            var pathParts = path.Split('/').ToList();

            // remove this first empty
            pathParts = pathParts.Skip(1).ToList();

            // transform from json
            pathParts = pathParts.Select(x => x.ToPascalCase()).ToList();

            var propertyName = string.Join(".", pathParts);

            return propertyName;
        }

        // Source: https://stackoverflow.com/questions/1196991/get-property-value-from-string-using-reflection
        public static object? GetPropertyValue(this object obj, string name)
        {
            foreach (string part in name.Split('.').Select(x => x.ToPascalCase()))
            {
                if (obj == null)
                {
                    return null;
                }

                Type type = obj.GetType();
                PropertyInfo? info = type.GetProperty(part);
                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public static T? GetPropertyValue<T>(this object obj, string name)
        {
            object? result = obj.GetPropertyValue(name);
            if (result == null)
            {
                return default(T);
            }

            // throws InvalidCastException if types are incompatible
            return (T)result;
        }

        public static T? GetPropertyValueFromPath<T>(this object obj, string path)
        {
            var propertyName = GetPropertyNameFromPath(path);

            return obj.GetPropertyValue<T>(propertyName);
        }

        public static Type? GetFirstGenericTypeArgumentFromPath(this object obj, string path)
        {
            var propertyName = GetPropertyNameFromPath(path);

            return obj.GetPropertyValue(propertyName)?.GetType().GenericTypeArguments.FirstOrDefault();
        }

        public static Type? GetFirstGenericTypeArgumentFromProperty(this object obj, string propertyName)
        {
            return obj.GetPropertyValue(propertyName)?.GetType().GenericTypeArguments.FirstOrDefault();
        }

        public static Type? ResolvePropertyTypeOfPropertyPath(this Type type, string propertyPath)
        {
            var propertyName = GetPropertyNameFromPath(propertyPath);

            return type.ResolvePropertyTypeOfPropertyName(propertyName);
        }

        public static Type? ResolvePropertyTypeOfPropertyName(
            this Type type,
            string propertyName,
            bool ensurePascalCase = true)
        {
            var propertyPath = propertyName.Split('.').Select(x => ensurePascalCase ? x.ToPascalCase() : x).ToArray();
            if (propertyPath.Length == 0)
            {
                throw new Exception($"propertyPath for property {propertyName} is not valid!");
            }

            var currentPropertyName = propertyPath.First();
            var propertyInfo = type.GetProperty(currentPropertyName);

            if (propertyInfo == null)
            {
                throw new Exception($"could not find property {currentPropertyName} in type {type.FullName}");
            }

            if (propertyPath.Length > 1)
            {
                if (propertyInfo.PropertyType.IsArray)
                {
                    // ToDo: optimize & test more!
                    var elementType = propertyInfo.PropertyType.GetElementType();
                    if (elementType == null)
                    {
                        return null;
                    }

                    if (propertyPath.Length == 2 && int.TryParse(propertyPath[1], out _))
                    {
                        return elementType;
                    }

                    var elementTypeElementType = elementType.GetElementType();

                    if (elementTypeElementType == null)
                    {
                        return null;
                    }

                    if (propertyPath.Length == 3 && int.TryParse(propertyPath[1], out _))
                    {
                        return elementTypeElementType;
                    }

                    var elementPropertyName = string.Join(".", propertyPath.Skip(3)); // skip List name and index
                    return ResolvePropertyTypeOfPropertyName(
                        elementTypeElementType,
                        elementPropertyName,
                        ensurePascalCase);
                }

                if (propertyInfo.PropertyType.IsEnumerable())
                {
                    var elementType = propertyInfo.PropertyType.GenericTypeArguments.First();
                    if (propertyPath.Length == 2 && int.TryParse(propertyPath[1], out _))
                    {
                        return elementType;
                    }

                    if (propertyPath.Length == 3 && int.TryParse(propertyPath[1], out _))
                    {
                        return elementType.GenericTypeArguments.First();
                    }

                    var elementPropertyName = string.Join(".", propertyPath.Skip(3)); // skip List name and index
                    return ResolvePropertyTypeOfPropertyName(
                        elementType.GenericTypeArguments.First(),
                        elementPropertyName,
                        ensurePascalCase);
                }

                var nextDirectPropertyName = propertyPath[1];
                string subPropertyName;
                Type? subType;
                if (int.TryParse(nextDirectPropertyName, out _))
                {
                    subPropertyName = string.Join(".", propertyPath.Skip(2)); // skip List name and index
                    subType = propertyInfo.PropertyType.GenericTypeArguments.FirstOrDefault();
                }
                else
                {
                    subPropertyName = string.Join(".", propertyPath.Skip(1));
                    subType = propertyInfo.PropertyType;
                }

                return subType?.ResolvePropertyTypeOfPropertyName(subPropertyName);
            }

            return propertyInfo.PropertyType;
        }

        public static List<PropertyInfo> GetPropertiesByCustomAttribute<T>(this Type type)
        {
            return type.GetProperties().Where(x =>
                    x.CustomAttributes.Any(y => y.AttributeType == typeof(T)))
                .ToList();
        }

        public static PropertyInfo? GetPropertyByCustomAttribute<T>(this Type type)
        {
            return type.GetProperties().FirstOrDefault(x =>
                x.CustomAttributes.Any(y => y.AttributeType == typeof(T)));
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        public static TResult InvokeGenericMethod<TResult>(
            this object target,
            string methodName,
            Type[] genericParameters,
            params object[] parameters)
        {
            var genericMethod = target.GetType().GetGenericMethod(methodName, genericParameters);
            var result = genericMethod.Invoke(
                target,
                parameters);
            return (TResult)result;
        }

        public static MethodInfo GetGenericMethod(
            this Type type,
            string methodName,
            params Type[] genericParameters)
        {
            // search the method in type and its base types
            Type? actualType = type;
            MethodInfo? methodInfo;
            do
            {
                methodInfo = actualType
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .FirstOrDefault(
                        x => x.Name == methodName && x.IsGenericMethod &&
                            x.GetGenericArguments().Length == genericParameters.Length);
                actualType = actualType.BaseType;
            }
            while (methodInfo == null && actualType != null);

            if (methodInfo == null)
            {
                throw new Exception($"could not find method {methodName} with {genericParameters.Length} generic parameters in type {type.FullName}");
            }

            var genericMethodInfo = methodInfo.MakeGenericMethod(genericParameters);
            return genericMethodInfo;
        }
    }
}
