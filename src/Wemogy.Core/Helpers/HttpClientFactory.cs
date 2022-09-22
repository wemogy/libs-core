using System;
using System.Net.Http;

namespace Wemogy.Core.Helpers
{
    public class HttpClientFactory
    {
        private HttpClient? _httpClient;

        public HttpClient GetHttpClient(Uri baseAddress, HttpMessageHandler? messageHandler = null, TimeSpan? timeout = null)
        {
            if (_httpClient == null)
            {
                if (messageHandler == null)
                {
                    _httpClient = new HttpClient()
                    {
                        BaseAddress = baseAddress
                    };
                }
                else
                {
                    _httpClient = new HttpClient(messageHandler)
                    {
                        BaseAddress = baseAddress
                    };
                }

                if (timeout.HasValue)
                {
                    _httpClient.Timeout = timeout.Value;
                }
            }

            return _httpClient;
        }
    }
}
