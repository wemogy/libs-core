using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Polly;
using Polly.Retry;

namespace Wemogy.Core.DynamicProxies
{
    public class RetryProxy<TException> : AsyncInterceptorBase
        where TException : Exception
    {
        private readonly AsyncRetryPolicy _retryPolicy;

        public RetryProxy(IEnumerable<TimeSpan> delay)
        {
            _retryPolicy = Policy
                .Handle<TException>()
                .WaitAndRetryAsync(delay);
        }

        protected override Task InterceptAsync(
            IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            return _retryPolicy.ExecuteAsync(
                () => proceed(
                    invocation,
                    proceedInfo));
        }

        protected override Task<TResult> InterceptAsync<TResult>(
            IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            return _retryPolicy.ExecuteAsync(
                () => proceed(
                    invocation,
                    proceedInfo));
        }
    }
}
