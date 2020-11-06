
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace Redpanda.OpenFaaS.Authentication
{
    public class ApiKeySecretAuthenticationOptions : AuthenticationSchemeOptions
    {
        [Required]
        public string SecretName { get; set; }

        [Required]
        public string ApiKeyHeader { get; set; }
        
        public string NameIdentifierHeader { get; set; }
    }
}
