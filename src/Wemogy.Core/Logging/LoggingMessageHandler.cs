using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wemogy.Core.Logging
{
    public class LoggingMessageHandler : DelegatingHandler
    {
        public LoggingMessageHandler(HttpMessageHandler? innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var builder = new StringBuilder();
            builder.Append("Request: " + request);
            if (request.Content != null)
            {
                builder.Append("\n" + await request.Content.ReadAsStringAsync());
            }

            Debug.WriteLine(builder.ToString());
            builder.Clear();

            var response = await base.SendAsync(request, cancellationToken);
            builder.Append("Response: " + response);
            if (response.Content != null)
            {
                builder.Append("\n" + await response.Content.ReadAsStringAsync());
            }

            Debug.WriteLine(builder.ToString());
            return response;
        }
    }
}
