using System;
using System.Linq;
using System.Reflection;
using FastExpressionCompiler;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Wemogy.Core.Extensions;
using Wemogy.Core.Mapster.Abstractions;

namespace Wemogy.Core.Mapster.Extensions
{
    public static class DependencyInjection
    {
        public static void AddMapster(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMapster(Assembly.GetCallingAssembly());
        }

        public static void AddMapster(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            // enable explicit mapping
            TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = true;

            // register all mapping profiles
            serviceCollection.AddMapsterConfigs(assemblies);

            // enable FastExpressionCompiler
            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();

            // validate config
            TypeAdapterConfig.GlobalSettings.Compile();
        }

        public static void AddMapsterConfigs(this IServiceCollection serviceCollection, Assembly[] assemblies)
        {
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
        }
    }
}
