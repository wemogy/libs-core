using System;
using System.Threading.Tasks;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace Wemogy.Core.Resilience
{
    public static class ReliableService
    {
        public static async Task RunExponential<TException>(Func<Task> task)
            where TException : Exception
        {
            var delay = Backoff.ExponentialBackoff(
                TimeSpan.FromMilliseconds(100),
                retryCount: 5);

            var retryPolicy = Policy
                .Handle<TException>()
                .WaitAndRetryAsync(delay);

            var policyResult = await retryPolicy.ExecuteAndCaptureAsync(task);

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                throw policyResult.FinalException;
            }
        }
    }
}
