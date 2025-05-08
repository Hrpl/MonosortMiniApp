using Microsoft.EntityFrameworkCore;
using MonosortMiniApp.Infrastructure.Context;

namespace MonosortMiniApp.API.Extensions;

public static class DbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder, IConfiguration configuration)
    {

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            configuration["ConnectionString:DefaultConnection"],
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
        ));

        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
