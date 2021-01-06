using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Authentication
{
    public static class SchemesNamesConst
    {
        public const string TokenAuthenticationDefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string APIKEYNAME = "ApiKey";

        public IServiceProvider ServiceProvider { get; set; }

        public TokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider)
            : base(options, logger, encoder, clock)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var headers = Request.Headers;
        
            var token = Request.Headers[APIKEYNAME];

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is null"));
            }

            // check token here
            bool isValidToken = false; 
            var appSettings = Request.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            if (token == apiKey)
                isValidToken = true;

            if (!isValidToken)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Balancer not authorize token : for token={token}"));
            }

            var claims = new[] { new Claim("token", token) };
            var identity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), this.Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
