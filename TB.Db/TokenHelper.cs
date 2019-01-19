using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TB.Db.Entities;
using ToBuy.Common.Enums;

namespace TB.Db
{
    public class TokenHelper
    {
        private static readonly byte[] key = Encoding.ASCII.GetBytes("d2xru0u4mn5nuikenab1");


        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer="blackneuron",
                Audience = "audience",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.UserRoles.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string result = tokenHandler.WriteToken(token);
            return result;
        }
        public static ClaimsPrincipal Validate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var param = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.FromMinutes(1),
                ValidIssuer = "blackneuron",
                ValidAudience = "audience",
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
            SecurityToken validatedToken; 
            var claims = tokenHandler.ValidateToken(token, param,out validatedToken);
            return claims;

        }
    }
}
