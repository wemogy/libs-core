using System;
using System.Linq;

namespace Wemogy.Core.Extensions
{
    public static class TypeExtensions
    {
        // Thanks to: https://stackoverflow.com/a/4897426/16056022
        public static bool InheritsOrImplements(this Type child, Type parent, out Type? type)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.IsGenericType
                ? child.GetGenericTypeDefinition()
                : child;

            while (currentChild != typeof(object))
            {
                Type? matchedInterface = null;
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild, out matchedInterface))
                {
                    type = matchedInterface ?? currentChild;
                    return true;
                }

                currentChild = currentChild.BaseType is { IsGenericType: true }
                    ? currentChild.BaseType.GetGenericTypeDefinition()
                    : currentChild.BaseType;

                if (currentChild == null)
                {
                    type = null;
                    return false;
                }
            }

            type = null;
            return false;
        }

        public static bool InheritsOrImplements(this Type child, Type parent)
        {
            return child.InheritsOrImplements(parent, out _);
        }

        private static bool HasAnyInterfaces(Type parent, Type child, out Type? matchedInterface)
        {
            matchedInterface = child.GetInterfaces()
                .FirstOrDefault(childInterface =>
                {
                    var currentInterface = childInterface.IsGenericType && parent.IsGenericType && parent.IsGenericTypeDefinition
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
            return matchedInterface != null;
        }

        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            bool shouldUseGenericType = !(parent.IsGenericType && parent.GetGenericTypeDefinition() != parent);

            if (parent.IsGenericType && shouldUseGenericType)
            {
                parent = parent.GetGenericTypeDefinition();
            }

            return parent;
        }
    }
}
