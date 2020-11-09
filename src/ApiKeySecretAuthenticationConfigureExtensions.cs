
using System;
using Redpanda.OpenFaaS.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiKeySecretAuthenticationConfigureExtensions
    {
        public static IServiceCollection ConfigureApiKeySecretScheme( this IServiceCollection services, Action<ApiKeySecretAuthenticationSchemeOptions> configure )
        {
            return services.Configure<ApiKeySecretAuthenticationSchemeOptions>( configure );
        }
    }
}
