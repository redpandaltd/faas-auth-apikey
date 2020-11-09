using System;
using Redpanda.OpenFaaS.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiKeySecretAuthenticationServiceExtensions
    {
        public static IServiceCollection AddApiKeySecretAuthentication( this IServiceCollection services, Action<ApiKeySecretAuthenticationSchemeOptions> configure )
        {
            services.AddAuthenticationCore( options =>
            {
                options.AddScheme<ApiKeySecretAuthenticationHandler>( "openfaas-secret-api-key", "API key from OpenFaaS secret" );
            } );

            services.ConfigureApiKeySecretScheme( schemeOptions =>
            {
                configure( schemeOptions );
            } );

            return ( services );
        }
    }
}
