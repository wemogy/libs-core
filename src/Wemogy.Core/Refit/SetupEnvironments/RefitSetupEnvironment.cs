using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Wemogy.Core.Refit.SetupEnvironments
{
    public class RefitSetupEnvironment
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly RefitEnvironment _refitEnvironment;

        public RefitSetupEnvironment(IServiceCollection serviceCollection, Uri baseAddress)
        {
            _serviceCollection = serviceCollection;
            _refitEnvironment = new RefitEnvironment(baseAddress);
        }

        public RefitSetupEnvironment WithDefaultRequestHeaders(Action<HttpRequestHeaders> setDefaultRequestHeaders)
        {
            _refitEnvironment.WithDefaultRequestHeaders(setDefaultRequestHeaders);
            return this;
        }

        public RefitSetupEnvironment ModifySettings(Action<RefitSettings> modifySettings)
        {
            _refitEnvironment.ModifySettings(modifySettings);
            return this;
        }

        public RefitSetupEnvironment WithInsecureLogging()
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            _refitEnvironment.WithInsecureLogging(serviceProvider);
            return this;
        }

        public RefitSetupEnvironment WithTimeout(TimeSpan timeout)
        {
            _refitEnvironment.WithTimeout(timeout);
            return this;
        }

        public RefitSetupEnvironment WithBearerToken(Func<string> bearerTokenGetter)
        {
            _refitEnvironment.WithBearerToken(bearerTokenGetter);
            return this;
        }

        public RefitSetupEnvironment WithApiInterface<TApiInterface>()
            where TApiInterface : class
        {
            var api = _refitEnvironment.GetApi<TApiInterface>();
            _serviceCollection.AddSingleton(api);
            return this;
        }
    }
}
