using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Ecom
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly string SecretKey = "123456ABCDEFGHIJKLMNOPQSRTUVWXYZ54321";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the request has an Authorization header
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            var token = authHeader.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Optionally, set the current principal if you need access to the claims in controllers.
                var jwtToken = validatedToken as JwtSecurityToken;
                var identity = new ClaimsIdentity(jwtToken.Claims, "Jwt");
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }
            catch (Exception)
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
        }
    }
}