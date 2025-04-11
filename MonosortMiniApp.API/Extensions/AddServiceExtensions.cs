using Infrastructure.Services.Implementations;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using MonosortMiniApp.API.Services.Implementations;
using MonosortMiniApp.API.Services.Interfaces;
using MonosortMiniApp.Domain.Commons.Options;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Hubs;
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

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // если запрос направлен хабу
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub/status"))
                        {
                            // получаем токен из строки запроса
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            builder.Services.AddAuthorization();
        }
        public static void AddOptionsSmtp(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Key));
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
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDrinkService, DrinkService>();
            services.AddScoped<IDessertService, DessertService>();
            services.AddScoped<IAdditiveService, AdditiveService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IConnectionService, ConnectionService>();
            services.AddScoped<OrderHub>();
        }
    }
}
