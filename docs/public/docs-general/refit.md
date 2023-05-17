# Refit

## Why

We created an abstraction for the **Refit** library, because we want to make the usage of the library more simple and unify the way how we use the library.

## How to use

### Add an Refit API to your dependency container

```csharp title="Startup.cs"
services
  // introduce a new API like `api.github.com`
  .AddRefitApi(new Uri("https://base-address-of-your-api.com"))
  // and add interfaces where the routes are defined
  .WithApiInterface<IUserApi>();
  .WithApiInterface<IResourceApi>();
```

For the definition of the interfaces, see the [Refit Documentation](https://github.com/reactiveui/refit)

```csharp title="IResourceApi.cs"
public interface IResourceApi
{
    [Get("/resources")]
    public Task<List<Resource>> GetAllAsync();

    [Get("/resources/{id}")]
    public Task<Resource> GetAsync(string id);
}
```

### Add insecure logging

For debugging purpose it can be helpful to see the requests and responses. To enable this feature just call `WithInsecureLogging()` right after `AddRefitApi()`.

```csharp title="Startup.cs"
services
  .AddRefitApi(new Uri("https://base-address-of-your-api.com"))
  .WithInsecureLogging()
  .WithApiInterface<ISampleApi>()
```

:::danger
Make sure the you have a LoggerFactory in your dependency container, because internally we log into `ILogger<>` instance.
:::

### Add Bearer token authentication

For authentication purposes it is necessary to add a Bearer token to each request. To do this, call `WithBearerToken()` right after `AddRefitApi()`. As argument you just pass a getter function that returns the Bearer token.

```csharp title="Startup.cs"
serviceCollection
  .AddRefitApi(new Uri("https://base-address-of-your-api.com"))
  .WithBearerToken(() => "ey1234...")
```
