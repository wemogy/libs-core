using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Wemogy.Core.Refit.MessageHandler.Interfaces
{
    public interface IRequestHandler
    {
        Task HandleRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}
