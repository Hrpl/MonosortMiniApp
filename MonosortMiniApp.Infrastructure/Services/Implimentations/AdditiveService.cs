using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class AdditiveService : IAdditiveService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "dictionary.Additive";
    public AdditiveService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }
    public async Task<List<AdditiveModel>> GetManyAdditiveAsync(int typeId)
    {
        var query = _query.Query(TableName)
            .Where("TypeAdditiveId", typeId)
            .Select("Id",
            "Name",
            "Price",
            "Photo",
            "IsExistence");

        var result = await _query.GetAsync<AdditiveModel>(query);
        return result.ToList();
    }

    public async Task<List<GetTypeAdditive>> GetTypeAdditiveAsync(int drinkId)
    {
        var queryDrinks = _query.Query("dictionary.Drinks")
            .Where("Id", drinkId)
            .Select("MenuId");

        var menuId = await _query.FirstOrDefaultAsync<int>(queryDrinks);

        var queryTA = _query.Query("dictionary.TypeAdditive")
            .When(drinkId != 1, q => q.WhereNotLike("Name", "%Эспрессо%"))
            .Select("Id",
            "Name",
            "Photo");

        var result = await _query.GetAsync<GetTypeAdditive>(queryTA);
        return result.ToList();
    }
}
