using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wemogy.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetClassTypesWhichImplementInterface(this Assembly assembly, Type interfaceType)
        {
            return GetClassTypesWhichImplementInterface(
                new List<Assembly>()
            {
                assembly
            },
                interfaceType);
        }

        public static List<Type> GetClassTypesWhichImplementInterface<TInterface>(this Assembly assembly)
        {
            var interfaceType = typeof(TInterface);
            return assembly.GetClassTypesWhichImplementInterface(interfaceType);
        }

        public static List<Type> GetClassTypesWhichImplementInterface(this List<Assembly> assemblies, Type interfaceType)
        {
            return assemblies
                .SelectMany(
                    assembly => assembly.GetTypes()
                    .Where(t => t.IsClass && t.InheritsOrImplements(interfaceType)))
                .ToList();
        }

        public static List<Type> GetClassTypesWhichImplementInterface<TInterface>(this List<Assembly> assemblies)
        {
            var interfaceType = typeof(TInterface);
            return assemblies.GetClassTypesWhichImplementInterface(interfaceType);
        }
    }
}
