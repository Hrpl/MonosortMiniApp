using Microsoft.EntityFrameworkCore;
using MonosortMiniApp.Infrastructure.Context;

namespace MonosortMiniApp.API.Extensions;

public static class DbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration["ConnectionString:DefaultConnection"],
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
        ));
    }
}
