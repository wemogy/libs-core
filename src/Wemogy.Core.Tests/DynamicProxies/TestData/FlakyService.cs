using System;
using System.Threading.Tasks;

namespace Wemogy.Core.Tests.DynamicProxies.TestData;

public class FlakyService : IFlakyService
{
    public static readonly string GetSomethingResult = "GetSomethingResult";

    private readonly int _failAttempts;
    private readonly Func<Exception> _exceptionFactory;
    private int _failAttemptsCounter;

    public FlakyService(int failAttempts, Func<Exception> exceptionFactory)
    {
        _failAttempts = failAttempts;
        _exceptionFactory = exceptionFactory;
    }

    public void DoSomething()
    {
        IncreaseAndCheckFailAttempts();

        // nothing to do
    }

    public string GetSomething()
    {
        IncreaseAndCheckFailAttempts();

        return GetSomethingResult;
    }

    public async Task DoSomethingAsync()
    {
        await Task.Delay(1000);

        IncreaseAndCheckFailAttempts();
    }

    public async Task<string> GetSomethingAsync()
    {
        await Task.Delay(1000);

        IncreaseAndCheckFailAttempts();

        return GetSomethingResult;
    }

    private void IncreaseAndCheckFailAttempts()
    {
        if (_failAttemptsCounter++ < _failAttempts)
        {
            throw _exceptionFactory();
        }
    }

    public void Reset()
    {
        _failAttemptsCounter = 0;
    }
}
