using System;
using System.Linq;
using System.Reflection;
using FastExpressionCompiler;
using Mapster;
using Wemogy.Core.Extensions;
using Wemogy.Core.Mapster.Abstractions;

namespace Wemogy.Core.Mapster
{
    public static class MapsterRegistration
    {
        /// <summary>
        /// Registers all IMappingConfig implementations in the executing assembly globally to Mapster.
        /// </summary>
        /// <param name="requireExplicitMapping">Enable explicit mapping</param>
        public static void RegisterMappings(bool requireExplicitMapping = false)
        {
            RegisterMappings(requireExplicitMapping, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Registers all IMappingConfig implementations in the given assemblies globally to Mapster.
        /// </summary>
        /// <param name="requireExplicitMapping">Enable explicit mapping</param>
        /// <param name="assemblies">The assemblies to include.</param>
        public static void RegisterMappings(bool requireExplicitMapping = false, params Assembly[] assemblies)
        {
            // enable explicit mapping
            TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = requireExplicitMapping;

            // register all mapping profiles
            // get all IMappingConfig implementations
            var mappingConfigs = assemblies
                .ToList()
                .GetClassTypesWhichImplementInterface<IMappingConfig>()
                .Select(x => Activator.CreateInstance(x) as IMappingConfig) !
                .ToList<IMappingConfig>();

            foreach (var mappingConfig in mappingConfigs)
            {
                mappingConfig.Configure(TypeAdapterConfig.GlobalSettings);
            }

            // enable FastExpressionCompiler
            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();

            // validate config
            TypeAdapterConfig.GlobalSettings.Compile();
        }
    }
}
