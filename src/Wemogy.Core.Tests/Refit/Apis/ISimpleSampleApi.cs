using System.Threading.Tasks;
using Refit;

namespace Wemogy.Core.Tests.Refit.Apis
{
    public interface ISimpleSampleApi
    {
        [Get("/headerDebug")]
        public Task<string> HeaderDebugAsync();
    }
}
