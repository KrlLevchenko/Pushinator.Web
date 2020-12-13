using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Pushinator.Web.AppStart
{
    public static class JwtConfig
    {
        public const string Issuer = "Pushinator";
        public const string Audience = "PushinatorApiClients";
        public static readonly SymmetricSecurityKey Key = 
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretkey!123")); // where to store secret???

        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Issuer,
 
                        ValidateAudience = true,
                        ValidAudience = Audience,

                        ValidateLifetime = true,
 
                        IssuerSigningKey = Key,
                        ValidateIssuerSigningKey = true,
                    };
                });

            return services;
        }
    }
}