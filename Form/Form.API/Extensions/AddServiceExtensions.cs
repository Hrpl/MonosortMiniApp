using Form.API.Services.Implementations;
using Form.API.Services.Interfaces;
using Form.Domain.Options;
using Form.Infrastructure.Services.Implementations;
using Form.Infrastructure.Services.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace Form.API.Extensions;

public static class AddServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddMapster();
        services.AddRegisterService();
    }
    public static void AddMapster(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        Mapper mapperConf = new(config);
        services.AddSingleton<IMapper>(mapperConf);
    }
    public static void AddOptionsSmtp(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Key));
    }
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddScoped<IDbConnectionManager, DbConnectionManager>();
        services.AddScoped<IFormService, FormService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
