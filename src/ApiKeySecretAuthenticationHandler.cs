
using System;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Redpanda.OpenFaaS.Authentication
{
    public class ApiKeySecretAuthenticationHandler : AuthenticationHandler<ApiKeySecretAuthenticationSchemeOptions>
    {
        private readonly string secretValue;
        private readonly string apiKeyHeader;
        private readonly string nameIdentifierHeader;

        public ApiKeySecretAuthenticationHandler( IOptionsMonitor<ApiKeySecretAuthenticationSchemeOptions> optionsAccessor
                , ILoggerFactory loggerFactory
                , UrlEncoder urlEncoder
                , ISystemClock systemClock )
            : base( optionsAccessor, loggerFactory, urlEncoder, systemClock )
        {
            var options = optionsAccessor.CurrentValue;

            apiKeyHeader = options.ApiKeyHeader;
            nameIdentifierHeader = options.NameIdentifierHeader;
            secretValue = options.Secret;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var apiKeyValue = Context.Request.Headers[apiKeyHeader];

            if ( string.IsNullOrEmpty( apiKeyValue ) )
            {
                return Task.FromResult( AuthenticateResult.NoResult() );
            }

            if ( apiKeyValue != secretValue )
            {
                return Task.FromResult( AuthenticateResult.Fail( new UnauthorizedAccessException() ) );
            }

            var identity = new ClaimsIdentity( "API Key" );

            if ( !string.IsNullOrEmpty( nameIdentifierHeader ) && Context.Request.Headers.ContainsKey( nameIdentifierHeader ) )
            {
                identity.AddClaim( new Claim( ClaimTypes.NameIdentifier, Context.Request.Headers[nameIdentifierHeader] ) );
            }

            var ticket = new AuthenticationTicket( new ClaimsPrincipal( identity ), "openfaas-secret-api-key" );

            return Task.FromResult( AuthenticateResult.Success( ticket ) );
        }
    }
}
