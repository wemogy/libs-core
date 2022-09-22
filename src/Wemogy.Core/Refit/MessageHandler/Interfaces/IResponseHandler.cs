using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Wemogy.Core.Refit.MessageHandler.Interfaces
{
    public interface IResponseHandler
    {
        Task HandleResponseAsync(HttpResponseMessage response, HttpRequestMessage request, CancellationToken cancellationToken);
    }
}
