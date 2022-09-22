using System.Threading.Tasks;
using Refit;

namespace Wemogy.Core.Tests.Refit.Apis
{
    public interface ISampleApi
    {
        [Get("/hello")]
        public Task<string> HelloAsync([Query] string name);

        [Get("/headerDebug")]
        public Task<string> HeaderDebugAsync([Query] string headerKey);
    }
}
