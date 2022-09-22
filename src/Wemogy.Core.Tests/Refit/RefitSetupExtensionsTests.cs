using System;
using System.Threading.Tasks;
using Divergic.Logging.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Wemogy.Core.Refit;
using Wemogy.Core.Refit.Logging;
using Wemogy.Core.Tests.Refit.Apis;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Wemogy.Core.Tests.Refit;

public class RefitSetupExtensionsTests : IDisposable
{
    private readonly WireMockServer _wireMockServer;

    public RefitSetupExtensionsTests()
    {
        _wireMockServer = WireMockServer.Start();
        ArrangeDefaultWireMockServer();
    }

    private void ArrangeDefaultWireMockServer()
    {
        _wireMockServer
            .Given(Request.Create().WithPath("/hello").UsingGet())
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBody("Hello {{request.query.name}}")
                    .WithTransformer());

        _wireMockServer
            .Given(Request.Create().WithPath("/headerDebug").UsingGet())
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBody("{{request.headers.Authorization}}")
                    .WithTransformer());
    }

    private Uri GetMockServerUri(string path = "")
    {
        return new Uri($"{_wireMockServer.Url}{path}");
    }

    [Fact]
    public async Task AddRefitApi_ShouldWork()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddRefitApi(GetMockServerUri())
            .WithApiInterface<ISampleApi>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var sampleApiService = serviceProvider.GetRequiredService<ISampleApi>();
        var message = await sampleApiService.HelloAsync("wemogy");

        // Assert
        Assert.Equal(
            "Hello wemogy",
            message);
    }

    [Fact]
    public async Task WithInsecureLogging_ShouldWork()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var mockedLoggingRequestHandlerLogger = Substitute.For<ILogger<LoggingRequestHandler>>().WithCache();
        mockedLoggingRequestHandlerLogger.IsEnabled(Arg.Any<LogLevel>()).Returns(true);
        serviceCollection.AddSingleton<ILogger<LoggingRequestHandler>>(mockedLoggingRequestHandlerLogger);
        var mockedLoggingResponseHandlerLogger = Substitute.For<ILogger<LoggingResponseHandler>>().WithCache();
        mockedLoggingResponseHandlerLogger.IsEnabled(Arg.Any<LogLevel>()).Returns(true);
        serviceCollection.AddSingleton<ILogger<LoggingResponseHandler>>(mockedLoggingResponseHandlerLogger);

        serviceCollection
            .AddRefitApi(GetMockServerUri())
            .WithInsecureLogging()
            .WithApiInterface<ISampleApi>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var sampleApiService = serviceProvider.GetRequiredService<ISampleApi>();
        var message = await sampleApiService.HelloAsync("wemogy");

        // Assert
        Assert.Equal(
            "Hello wemogy",
            message);
        Assert.Equal(
            4,
            mockedLoggingRequestHandlerLogger.Count);
        Assert.Equal(
            8,
            mockedLoggingResponseHandlerLogger.Count);
    }

    [Fact]
    public async Task WithInsecureLogging_ShouldWork_WhenNoLoggerIsPresentInDI()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddRefitApi(GetMockServerUri())
            .WithInsecureLogging()
            .WithApiInterface<ISampleApi>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var sampleApiService = serviceProvider.GetRequiredService<ISampleApi>();
        var message = await sampleApiService.HelloAsync("wemogy");

        // Assert
        Assert.Equal(
            "Hello wemogy",
            message);
    }

    [Fact]
    public async Task WithBearerToken_ShouldWork()
    {
        // Arrange
        var bearerToken = "ey1234";
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddRefitApi(GetMockServerUri())
            .WithBearerToken(() => bearerToken)
            .WithApiInterface<ISampleApi>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var sampleApiService = serviceProvider.GetRequiredService<ISampleApi>();
        var headerValue = await sampleApiService.HeaderDebugAsync("Authorization");

        // Assert
        Assert.Equal(
            $"Bearer {bearerToken}",
            headerValue);
    }

    public void Dispose()
    {
        _wireMockServer.Stop();
    }
}
