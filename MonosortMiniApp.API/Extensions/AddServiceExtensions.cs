﻿using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MonosortMiniApp.Infrastructure.Services.Implimentations;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace MonosortMiniApp.API.Extensions
{
    public static class AddServiceExtensions
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddMapster();
            services.AddRegisterService();
        }
        public static void AddJwt(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JwtConfigurations:Issuer"],
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfigurations:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();
        }

        public static void AddMapster(this IServiceCollection services)
        {
            TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            Mapper mapperConf = new(config);
            services.AddSingleton<IMapper>(mapperConf);
        }

        public static void AddRegisterService(this IServiceCollection services)
        {
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IDbConnectionManager, DbConnectionManager>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
