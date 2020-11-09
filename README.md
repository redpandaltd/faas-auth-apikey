# OpenFaaS API Key Authentication Extension

This is part of OpenFaaS C# Template. It provides API Key authentication based on a secret.

## Installing

Add a package reference from NuGet

```
dotnet add package Redpanda.OpenFaaS.Extensions.Authentication
```

## Usage

Unless you need to customize the authentication further, it can be configured with one extension method in the `Startup.cs` file.

```csharp
public void ConfigureServices( IServiceCollection services )
{
    services.AddTransient<IHttpFunction, Function>();

    // add your services here.
    services.AddApiKeySecretAuthentication( schemeOptions =>
    {
        schemeOptions.SecretName = "my-secret";
        schemeOptions.ApiKeyHeader = "X-Api-Key";
    } );
}
```

The `SecretName` is the name of the secret where the expected API key is stored. The `X-Api-Key` is the name of the header where to look for the actual API key used by the client. If they match, `HttpContext.User` is populated with an authenticated identity.

Next, we need to let the handler know that the function requires authorization. We do this by decorating the function with the `Authorize` attribute from ASPNET.

```csharp
namespace OpenFaaS
{
    [Authorize]
    public class Function : HttpFunction
    {
        ...
    }
}
```

By doing this, all requests where the API key doesn't match the secret value will return a 401 response.

### User Id

If we set up the `NameIdentifierHeader`, its value will be populated into the user claims as a `NameIdenfifier` claim.

```csharp
    services.AddApiKeySecretAuthentication( schemeOptions =>
    {
        ...
        schemeOptions.NameIdentifierHeader = "X-User-Id";
    } );
```
