using Microsoft.EntityFrameworkCore;
using MonosortMiniApp.Infrastructure.Context;

namespace MonosortMiniApp.API.Extensions;

public static class DbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            "Host=85.208.87.10;Port=5432;Database=MonosortMiniApp;Username=postgres;Password=2208;",
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
        ));

        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
