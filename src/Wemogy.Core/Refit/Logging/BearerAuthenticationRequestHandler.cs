using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Wemogy.Core.Refit.MessageHandler.Interfaces;

namespace Wemogy.Core.Refit.Logging
{
    public class BearerAuthenticationRequestHandler : IRequestHandler
    {
        private readonly Func<string> _bearerTokenGetter;

        public BearerAuthenticationRequestHandler(Func<string> bearerTokenGetter)
        {
            _bearerTokenGetter = bearerTokenGetter;
        }

        public Task HandleRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _bearerTokenGetter();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return Task.CompletedTask;
        }
    }
}
