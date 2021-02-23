using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Pushinator.Web.AppStart;

namespace Pushinator.Web.Core.Auth
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(ClaimsIdentity claimIdentity)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthConfig.Issuer,
                audience: AuthConfig.Audience,
                notBefore: DateTime.UtcNow,
                claims: claimIdentity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(24)),
                signingCredentials: new SigningCredentials(AuthConfig.Key, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}