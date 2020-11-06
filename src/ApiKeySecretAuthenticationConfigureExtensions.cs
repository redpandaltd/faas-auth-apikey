
using System;
using Redpanda.OpenFaaS.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiKeySecretAuthenticationConfigureExtensions
    {
        public static IServiceCollection ConfigureApiKeySecretScheme( this IServiceCollection services, Action<ApiKeySecretAuthenticationOptions> configure )
        {
            return services.Configure<ApiKeySecretAuthenticationOptions>( configure );
        }
    }
}
