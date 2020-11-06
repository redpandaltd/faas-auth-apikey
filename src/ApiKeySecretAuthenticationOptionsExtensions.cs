
using System;
using Redpanda.OpenFaaS.Authentication;

namespace Microsoft.AspNetCore.Authentication
{
    public static class ApiKeySecretAuthenticationOptionsExtensions
    {
        public static void AddApiKeySecretScheme( this AuthenticationOptions options, Action<ApiKeySecretAuthenticationOptions> configure )
        {
            options.AddScheme<ApiKeySecretAuthenticationHandler>( "openfaas-secret-api-key", "API key from OpenFaaS secret" );
        }
    }
}
