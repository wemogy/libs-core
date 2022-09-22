using System;
using System.Threading.Tasks;
using FluentAssertions;
using Polly.Contrib.WaitAndRetry;
using Wemogy.Core.DynamicProxies;
using Wemogy.Core.DynamicProxies.Extensions;
using Wemogy.Core.Errors;
using Wemogy.Core.Errors.Exceptions;
using Wemogy.Core.Tests.DynamicProxies.TestData;
using Xunit;

namespace Wemogy.Core.Tests.DynamicProxies;

public class RetryProxyTests
{
    private readonly Func<Exception> _exceptionFactory = () => Error.PreconditionFailed(
        "PreconditionFailed",
        "PreconditionFailed");

    [Fact]
    public async Task RetryProxy_ShouldWorkWhenAlwaysSuccessful()
    {
        // Arrange
        var flakyService = new FlakyService(
            0,
            _exceptionFactory);
        var retryProxy = new RetryProxy<PreconditionFailedErrorException>(
            Backoff.ExponentialBackoff(
                TimeSpan.FromSeconds(2),
                5));
        var wrappedFlakyService = retryProxy.Wrap<IFlakyService>(flakyService);

        // Act
        var doSomethingException = Record.Exception(() => wrappedFlakyService.DoSomething());
        flakyService.Reset();
        var doSomethingAsyncException = await Record.ExceptionAsync(() => wrappedFlakyService.DoSomethingAsync());
        flakyService.Reset();
        var getSomethingResult = wrappedFlakyService.GetSomething();
        flakyService.Reset();
        var getSomethingAsyncResult = await wrappedFlakyService.GetSomethingAsync();
        flakyService.Reset();

        // Assert
        doSomethingException.Should().BeNull();
        doSomethingAsyncException.Should().BeNull();
        getSomethingResult.Should().Be(FlakyService.GetSomethingResult);
        getSomethingAsyncResult.Should().Be(FlakyService.GetSomethingResult);
    }

    [Fact]
    public async Task RetryProxy_ShouldWorkWhenFailsUnderMaxFailedAttempts()
    {
        // Arrange
        var flakyService = new FlakyService(
            3,
            _exceptionFactory);
        var retryProxy = new RetryProxy<PreconditionFailedErrorException>(
            Backoff.ExponentialBackoff(
                TimeSpan.FromMilliseconds(200),
                5));
        var wrappedFlakyService = retryProxy.Wrap<IFlakyService>(flakyService);

        // Act
        var doSomethingException = Record.Exception(() => wrappedFlakyService.DoSomething());
        flakyService.Reset();
        var doSomethingAsyncException = await Record.ExceptionAsync(() => wrappedFlakyService.DoSomethingAsync());
        flakyService.Reset();
        var getSomethingResult = wrappedFlakyService.GetSomething();
        flakyService.Reset();
        var getSomethingAsyncResult = await wrappedFlakyService.GetSomethingAsync();
        flakyService.Reset();

        // Assert
        doSomethingException.Should().BeNull();
        doSomethingAsyncException.Should().BeNull();
        getSomethingResult.Should().Be(FlakyService.GetSomethingResult);
        getSomethingAsyncResult.Should().Be(FlakyService.GetSomethingResult);
    }

    [Fact]
    public async Task RetryProxy_ShouldWorkWhenFailsAboveMaxFailedAttempts()
    {
        // Arrange
        var flakyService = new FlakyService(
            5,
            _exceptionFactory);
        var retryProxy = new RetryProxy<PreconditionFailedErrorException>(
            Backoff.ExponentialBackoff(
                TimeSpan.FromMilliseconds(200),
                1));
        var wrappedFlakyService = retryProxy.Wrap<IFlakyService>(flakyService);

        // Act
        var doSomethingException = Record.Exception(() => wrappedFlakyService.DoSomething());
        flakyService.Reset();
        var doSomethingAsyncException = await Record.ExceptionAsync(() => wrappedFlakyService.DoSomethingAsync());
        flakyService.Reset();
        var getSomethingException = Record.Exception(() => wrappedFlakyService.GetSomething());
        flakyService.Reset();
        var getSomethingAsyncException = await Record.ExceptionAsync(() => wrappedFlakyService.GetSomethingAsync());
        flakyService.Reset();

        // Assert
        doSomethingException.Should().NotBeNull();
        doSomethingException.Should().BeOfType<PreconditionFailedErrorException>();
        doSomethingAsyncException.Should().NotBeNull();
        doSomethingAsyncException.Should().BeOfType<PreconditionFailedErrorException>();
        getSomethingException.Should().NotBeNull();
        getSomethingException.Should().BeOfType<PreconditionFailedErrorException>();
        getSomethingAsyncException.Should().NotBeNull();
        getSomethingAsyncException.Should().BeOfType<PreconditionFailedErrorException>();
    }
}
