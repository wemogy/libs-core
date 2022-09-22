using System;
using Microsoft.Extensions.DependencyInjection;
using Wemogy.Core.Refit.SetupEnvironments;

namespace Wemogy.Core.Refit
{
    public static class RefitSetupExtensions
    {
        public static RefitSetupEnvironment AddRefitApi(this IServiceCollection serviceCollection, Uri baseAddress)
        {
            return new RefitSetupEnvironment(serviceCollection, baseAddress);
        }
    }
}
