using System.Threading.Tasks;

namespace Wemogy.Core.Tests.DynamicProxies.TestData;

public interface IFlakyService
{
    public void DoSomething();
    public string GetSomething();
    public Task DoSomethingAsync();
    public Task<string> GetSomethingAsync();
}
