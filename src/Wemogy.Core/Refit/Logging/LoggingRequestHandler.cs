using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wemogy.Core.Refit.MessageHandler.Interfaces;

namespace Wemogy.Core.Refit.Logging
{
    public class LoggingRequestHandler : IRequestHandler
    {
        private readonly ILogger<LoggingRequestHandler> _logger;

        public LoggingRequestHandler(ILogger<LoggingRequestHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var req = request;
            var isTextBasedContentType = false;
            var id = Guid.NewGuid().ToString();
            var msg = $"[{id} -   Request]";

            var host = $"{req.RequestUri.Scheme}://{req.RequestUri.Host}:{req.RequestUri.Port}";
            _logger.LogDebug("{Msg}========Start==========", msg);
            _logger.LogDebug("{Msg} {ReqMethod} {RequestUriPathAndQuery} {RequestUriScheme}/{ReqVersion}", msg, req.Method, req.RequestUri.PathAndQuery, req.RequestUri.Scheme, req.Version);
            _logger.LogDebug("{Msg} Host: {Host}", msg, host);
            _logger.LogDebug("{Msg} {ReqMethod}: {Host}{RequestUriPathAndQuery}", msg, req.Method, host, req.RequestUri.PathAndQuery);

            foreach (var header in req.Headers)
            {
                _logger.LogDebug("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));
            }

            if (req.Content != null)
            {
                foreach (var header in req.Content.Headers)
                {
                    _logger.LogDebug("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));
                }

                isTextBasedContentType = req.Content is StringContent || IsTextBasedContentType(req.Headers) ||
                                        IsTextBasedContentType(req.Content.Headers);
                if (isTextBasedContentType)
                {
                    var result = await req.Content.ReadAsStringAsync();

                    _logger.LogDebug("{Msg} Content:", msg);
                    _logger.LogDebug("{Msg} {Join}...", msg, string.Join(string.Empty, result.Take(255)));
                }
            }

            req.Properties.Add(LoggingConstants.RequestIdPropertyKey, id);
            req.Properties.Add(LoggingConstants.StopwatchPropertyKey, Stopwatch.StartNew());
            req.Properties.Add(LoggingConstants.IsTextBasedContentTypePropertyKey, isTextBasedContentType);
        }

        readonly string[] _types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        bool IsTextBasedContentType(HttpHeaders headers)
        {
            if (!headers.TryGetValues("Content-Type", out var values))
            {
                return false;
            }

            var header = string.Join(" ", values).ToLowerInvariant();

            return _types.Any(t => header.Contains(t));
        }
    }
}
