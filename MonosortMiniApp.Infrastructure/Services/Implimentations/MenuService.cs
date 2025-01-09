using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class MenuService : IMenuService
{
    private readonly QueryFactory _query;
    public MenuService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<List<MenuResponse>> GetCategoriesAsync()
    {
        var query = _query.Query("dictionary.Menu")
            .Select("Id",
            "Name");

        var result = await _query.GetAsync<MenuResponse>(query);
        return result.ToList();
    }
}
