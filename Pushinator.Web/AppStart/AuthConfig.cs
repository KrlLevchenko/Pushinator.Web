using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Pushinator.Web.AppStart
{
    public static class AuthConfig
    {
        public const string Issuer = "Pushinator";
        public const string Audience = "PushinatorApiClients";
        public static readonly SymmetricSecurityKey Key = 
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretkey!123")); // where to store secret???

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
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
            });//.AddGoogle(options =>
            // {
            //     options.ClientId = configuration["AuthOptions:Google:ClientId"];
            //     options.ClientSecret = configuration["AuthOptions:Google:ClientSecret"];
            // })
            // .AddFacebook(options =>
            // {
            //     options.ClientId = configuration["AuthOptions:Facebook:ClientId"];
            //     options.ClientSecret = configuration["AuthOptions:Facebook:ClientSecret"];
            // })
            // .AddMicrosoftAccount(options =>
            // {
            //     options.ClientId = configuration["AuthOptions:Microsoft:ClientId"];
            //     options.ClientSecret = configuration["AuthOptions:Microsoft:ClientSecret"];
            // });
            
            return services;
        }
    }
}