using Microsoft.IdentityModel.Tokens;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        public static string GenerateJwtToken(string email)
        {
            var key = Encoding.ASCII.GetBytes("jOj_J?CZjv$sY?3t^?3f9o0R>G!NmkWnlil"); // Use a secure key!
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }),
                Expires = DateTime.UtcNow.AddHours(1), // Token valid for 1 hour
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}