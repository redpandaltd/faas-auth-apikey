
using Redpanda.OpenFaaS.Authentication;

namespace Microsoft.AspNetCore.Authentication
{
    public static class ApiKeySecretAuthenticationOptionsExtensions
    {
        public static void AddApiKeySecretScheme( this AuthenticationOptions options )
        {
            options.AddScheme<ApiKeySecretAuthenticationHandler>( "openfaas-secret-api-key", "API key from OpenFaaS secret" );
        }
    }
}
