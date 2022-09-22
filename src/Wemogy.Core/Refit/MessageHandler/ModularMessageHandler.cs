using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Wemogy.Core.Refit.MessageHandler.Interfaces;

namespace Wemogy.Core.Refit.MessageHandler
{
    public class ModularMessageHandler : DelegatingHandler
    {
        private readonly List<IRequestHandler> _requestHandlers;
        private readonly List<IResponseHandler> _responseHandlers;

        public bool IsEmpty => !_requestHandlers.Any() && !_responseHandlers.Any();

        public ModularMessageHandler(HttpMessageHandler? innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            _requestHandlers = new List<IRequestHandler>();
            _responseHandlers = new List<IResponseHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            foreach (var requestHandler in _requestHandlers)
            {
                await requestHandler.HandleRequestAsync(request, cancellationToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            foreach (var responseHandler in _responseHandlers)
            {
                await responseHandler.HandleResponseAsync(response, request, cancellationToken);
            }

            return response;
        }

        public void RegisterRequestHandler(IRequestHandler requestHandler)
        {
            _requestHandlers.Add(requestHandler);
        }

        public void RegisterResponseHandler(IResponseHandler responseHandler)
        {
            _responseHandlers.Add(responseHandler);
        }

        public ModularMessageHandler? GetIfNotEmpty()
        {
            if (IsEmpty)
            {
                return null;
            }

            return this;
        }
    }
}
