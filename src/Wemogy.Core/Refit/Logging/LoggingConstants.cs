using System.Diagnostics;
using System.Net.Http;

namespace Wemogy.Core.Refit.Logging
{
    public static class LoggingConstants
    {
        public static readonly HttpRequestOptionsKey<string> RequestIdPropertyKey = new("WemogyLoggingRequestId");
        public static readonly HttpRequestOptionsKey<Stopwatch> StopwatchPropertyKey = new("WemogyLoggingHandlerStopwatch");
        public static readonly HttpRequestOptionsKey<bool> IsTextBasedContentTypePropertyKey = new("WemogyLoggingIsTextBasedContentType");
    }
}
