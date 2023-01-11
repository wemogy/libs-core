using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using Wemogy.Core.Helpers;
using Wemogy.Core.Json;
using Wemogy.Core.Refit.Logging;
using Wemogy.Core.Refit.MessageHandler;

namespace Wemogy.Core.Refit
{
    public class RefitEnvironment
    {
        private readonly Uri _baseAddress;
        private readonly HttpClientFactory _httpClientFactory;
        private readonly ModularMessageHandler _modularMessageHandler;
        private HttpMessageHandler? _customMessageHandler;
        private Action<RefitSettings>? _modifySettings;
        private Action<HttpRequestHeaders>? _setHttpRequestHeaders;
        private TimeSpan? _timeout;

        public RefitEnvironment(Uri baseAddress)
        {
            _baseAddress = baseAddress;
            _customMessageHandler = null;
            _modifySettings = null;
            _httpClientFactory = new HttpClientFactory();
            _modularMessageHandler = new ModularMessageHandler();
            _setHttpRequestHeaders = null;
        }

        public RefitEnvironment WithDefaultRequestHeaders(Action<HttpRequestHeaders> setHttpRequestHeaders)
        {
            _setHttpRequestHeaders = setHttpRequestHeaders;
            return this;
        }

        public RefitEnvironment ModifySettings(Action<RefitSettings> modifySettings)
        {
            _modifySettings = modifySettings;
            return this;
        }

        public RefitEnvironment WithInsecureLogging(IServiceProvider? serviceProvider = null)
        {
            Console.WriteLine("Danger: Insecure logging is enabled. Do not use this in production!");

            // try to get a logger form DI, use NullLogger, if not found or no DI present
            var requestLogger = serviceProvider?.GetService<ILogger<LoggingRequestHandler>>() ??
                                NullLogger<LoggingRequestHandler>.Instance;
            var responseLogger = serviceProvider?.GetService<ILogger<LoggingResponseHandler>>() ??
                                NullLogger<LoggingResponseHandler>.Instance;

            // register the request and response handler for logging
            _modularMessageHandler.RegisterRequestHandler(new LoggingRequestHandler(requestLogger));
            _modularMessageHandler.RegisterResponseHandler(new LoggingResponseHandler(responseLogger));
            return this;
        }

        /// <summary>
        /// If you are using a custom message handler, the following features will not not work:
        /// - WithLogging()
        /// - WithBearerToken(()=> token)
        /// </summary>
        public RefitEnvironment WithCustomMessageHandler(HttpMessageHandler customMessageHandler)
        {
            _customMessageHandler = customMessageHandler;
            return this;
        }

        public RefitEnvironment WithTimeout(TimeSpan timeout)
        {
            _timeout = timeout;
            return this;
        }

        public RefitEnvironment WithBearerToken(Func<string> bearerTokenGetter)
        {
            _modularMessageHandler.RegisterRequestHandler(new BearerAuthenticationRequestHandler(bearerTokenGetter));
            return this;
        }

        public TApi GetApi<TApi>()
        {
            var settings = new RefitSettings(new SystemTextJsonContentSerializer(WemogyJson.Options));

            _modifySettings?.Invoke(settings);

            var messageHandler = _customMessageHandler ?? _modularMessageHandler.GetIfNotEmpty();

            var httpClient = _httpClientFactory.GetHttpClient(_baseAddress, messageHandler, _timeout);

            _setHttpRequestHeaders?.Invoke(httpClient.DefaultRequestHeaders);

            return RestService.For<TApi>(httpClient, settings);
        }
    }
}
