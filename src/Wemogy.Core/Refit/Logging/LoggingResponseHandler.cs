using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wemogy.Core.Refit.MessageHandler.Interfaces;

namespace Wemogy.Core.Refit.Logging
{
    public class LoggingResponseHandler : IResponseHandler
    {
        private readonly ILogger<LoggingResponseHandler> _logger;

        public LoggingResponseHandler(ILogger<LoggingResponseHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleResponseAsync(
            HttpResponseMessage response,
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var id = request.Properties[LoggingConstants.RequestIdPropertyKey];
            var msg = $"[{id} - Response]";
            var requestStopwatch = (Stopwatch)request.Properties[LoggingConstants.StopwatchPropertyKey];
            _logger.LogDebug("{Msg} Duration: {RequestStopwatchElapsedMilliseconds}", msg, requestStopwatch.ElapsedMilliseconds);
            _logger.LogDebug("{Msg}==========End==========", msg);

            _logger.LogDebug("{Msg}=========Start=========", msg);

            var resp = response;

            _logger.LogDebug("{Msg} {Upper}/{RespVersion} {RespStatusCode} {RespReasonPhrase}", msg, request.RequestUri.Scheme.ToUpper(), resp.Version, (int)resp.StatusCode, resp.ReasonPhrase);

            foreach (var header in resp.Headers)
            {
                _logger.LogDebug("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));
            }

            if (resp.Content != null)
            {
                foreach (var header in resp.Content.Headers)
                {
                    _logger.LogDebug("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));
                }

                var isTextBasedContentType =
                    (bool)request.Properties[LoggingConstants.IsTextBasedContentTypePropertyKey];
                if (isTextBasedContentType)
                {
                    var stopwatch = Stopwatch.StartNew();
                    var result = await resp.Content.ReadAsStringAsync();

                    _logger.LogDebug("{Msg} Content:", msg);
                    _logger.LogDebug("{Msg} {Join}...", msg, string.Join(string.Empty, result.Take(255)));
                    _logger.LogDebug("{Msg} Duration: {StopwatchElapsedMilliseconds}", msg, stopwatch.ElapsedMilliseconds);
                }
            }

            _logger.LogDebug("{Msg}==========End==========", msg);
        }
    }
}
