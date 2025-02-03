using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Ecom
{
    public class TokenManager
    {
        private static readonly string SecretKey = "123456ABCDEFGHIJKLMNOPQSRTUVWXYZ54321";

        public static string GenerateToken(string username, int expireMinutes = 60)
        {
            var key = Encoding.ASCII.GetBytes(SecretKey);

            // Create token descriptor with claims, expiration and signing credentials.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                // Add any claims you need
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}