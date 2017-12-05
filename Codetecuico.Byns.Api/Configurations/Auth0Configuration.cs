using Microsoft.AspNetCore.Builder;

namespace Codetecuico.Byns.Api
{
    public static class Auth0Configuration
    {
        public static void Configure(IApplicationBuilder app)
        {
            //var issuer = "https://" + ConfigurationManager.AppSettings["auth0:Domain"] + "/";
            //var audience = ConfigurationManager.AppSettings["auth0:ClientId"];
            //var secret = TextEncodings.Base64.Encode(TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["auth0:ClientSecret"]));

            //// Api controllers with an [Authorize] attribute will be validated with JWT
            //app.UseJwtBearerAuthentication(
            //        new JwtBearerAuthenticationOptions
            //        {
            //            AuthenticationMode = AuthenticationMode.Active,
            //            AllowedAudiences = new[] { audience },
            //            IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
            //            {
            //            new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
            //            }
            //        });
        }
    }
}