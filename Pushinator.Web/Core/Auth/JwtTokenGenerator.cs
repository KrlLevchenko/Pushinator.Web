using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Pushinator.Web.AppStart;

namespace Pushinator.Web.Core.Auth
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(string userId, string login)
        {
            var claimIdentity = new ClaimsIdentity();
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, login));
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            
            var jwt = new JwtSecurityToken(
                issuer: JwtConfig.Issuer,
                audience: JwtConfig.Audience,
                notBefore: DateTime.UtcNow,
                claims: claimIdentity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(24)),
                signingCredentials: new SigningCredentials(JwtConfig.Key, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}