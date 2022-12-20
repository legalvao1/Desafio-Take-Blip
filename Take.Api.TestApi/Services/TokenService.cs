using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using Take.Api.TestApi.Models;
using Take.Api.TestApi.Models.UI;

namespace Take.Api.TestApi.Services
{
    /// <summary>
    /// Service that generates a jwt token to user be able to make requests at this api endpoints
    /// </summary>
    public static class TokenService
    {
        /// <summary>
        /// Generates a jwt token to user be able to make requests at this api endpoints
        /// </summary>
        public static string GenerateToken(User user, ApiSettings settings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
