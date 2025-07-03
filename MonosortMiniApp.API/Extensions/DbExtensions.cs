using Microsoft.EntityFrameworkCore;
using MonosortMiniApp.Domain.Entities;
using MonosortMiniApp.Infrastructure.Context;
using System.Xml.Linq;

namespace MonosortMiniApp.API.Extensions;

public static class DbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var dbName = Environment.GetEnvironmentVariable("DB_CLIENT_NAME");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql($"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword};",
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
        ));

        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
