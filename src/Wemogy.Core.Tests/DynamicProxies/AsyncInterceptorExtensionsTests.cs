using System;
using FluentAssertions;
using Polly.Contrib.WaitAndRetry;
using Wemogy.Core.DynamicProxies;
using Wemogy.Core.DynamicProxies.Extensions;
using Wemogy.Core.Errors.Exceptions;
using Wemogy.Core.Tests.DynamicProxies.TestData;
using Xunit;

namespace Wemogy.Core.Tests.DynamicProxies;

public class AsyncInterceptorExtensionsTests
{
    [Fact]
    public void Wrap_ShouldReuseTheProxyTypeAcrossCalls()
    {
        // Arrange
        var retryProxy = new RetryProxy<PreconditionFailedErrorException>(
            Backoff.ExponentialBackoff(TimeSpan.FromMilliseconds(10), 1));

        // Act
        var first = retryProxy.Wrap<IFlakyService>(new FlakyService(0, () => new Exception()));
        var second = retryProxy.Wrap<IFlakyService>(new FlakyService(0, () => new Exception()));
        var untyped = retryProxy.Wrap(typeof(IFlakyService), new FlakyService(0, () => new Exception()));

        // Assert
        // A new ProxyGenerator per call emits a new dynamic assembly with a new proxy type every time.
        // Those types can never be unloaded, so a wrapper created per request (e.g. a scoped
        // repository) grows the process until it runs out of memory.
        second.GetType().Should().BeSameAs(first.GetType());
        untyped.GetType().Should().BeSameAs(first.GetType());
    }
}
