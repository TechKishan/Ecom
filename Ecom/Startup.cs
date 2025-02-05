using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecom
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure authentication
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            var key = System.Text.Encoding.ASCII.GetBytes("jOj_J?CZjv$sY?3t^?3f9o0R>G!NmkWnlil");

            app.UseJwtBearerAuthentication(new Microsoft.Owin.Security.Jwt.JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = System.TimeSpan.Zero
                }
            });
        }
    }
}