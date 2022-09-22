using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Wemogy.Core.DynamicProxies.Enums;

namespace Wemogy.Core.DynamicProxies
{
    public class FlakyProxy : AsyncInterceptorBase
    {
        public int FailAttempts { get; private set; }

        private readonly int _failAttempts;
        private readonly FlakyStrategy _strategy;
        private readonly Func<Exception> _exceptionFactory;
        private readonly List<string> _methodNameFilter;

        public FlakyProxy(int failAttempts, FlakyStrategy strategy, Func<Exception> exceptionFactory)
        {
            _failAttempts = failAttempts;
            _strategy = strategy;
            _exceptionFactory = exceptionFactory;
            _methodNameFilter = new List<string>();
        }

        private void IncreaseAndCheckFailAttempts(FlakyStrategy strategy, IInvocation invocation)
        {
            if (_strategy != strategy)
            {
                return;
            }

            if (_methodNameFilter.Any() && !_methodNameFilter.Contains(invocation.Method.Name))
            {
                return;
            }

            if (FailAttempts < _failAttempts)
            {
                FailAttempts++;
                throw _exceptionFactory();
            }
        }

        public void Reset()
        {
            FailAttempts = 0;
        }

        public FlakyProxy OnlyForMethodsWithName(params string[] methodNames)
        {
            _methodNameFilter.AddRange(methodNames);
            return this;
        }

        protected override async Task InterceptAsync(
            IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            IncreaseAndCheckFailAttempts(
                FlakyStrategy.ThrowBeforeInvocation,
                invocation);
            await proceed(
                invocation,
                proceedInfo);
            IncreaseAndCheckFailAttempts(
                FlakyStrategy.ThrowAfterInvocation,
                invocation);
        }

        protected override async Task<TResult> InterceptAsync<TResult>(
            IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            IncreaseAndCheckFailAttempts(
                FlakyStrategy.ThrowBeforeInvocation,
                invocation);
            var result = await proceed(
                invocation,
                proceedInfo);
            IncreaseAndCheckFailAttempts(
                FlakyStrategy.ThrowAfterInvocation,
                invocation);
            return result;
        }
    }
}
