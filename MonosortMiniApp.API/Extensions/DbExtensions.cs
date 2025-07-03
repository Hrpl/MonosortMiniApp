using Microsoft.EntityFrameworkCore;
using MonosortMiniApp.Infrastructure.Context;

namespace MonosortMiniApp.API.Extensions;

public static class DbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(dbHost,
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
        ));

        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
