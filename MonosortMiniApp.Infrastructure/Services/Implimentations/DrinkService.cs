using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class DrinkService : IDrinkService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "dictionary.Drinks";
    public DrinkService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }
    public async Task<List<GetManyDrinksModel>> GetManyDrinksAsync(int typeId)
    {
        var query = _query.Query(TableName)
            .Where("TypeDrinkId", typeId)
            .Where("IsDeleted", false)
            .Select("Id",
            "Name",
            "Photo",
            "IsExistence");

        var result = await _query.GetAsync<GetManyDrinksModel>(query);

        return result.ToList();
    }
}
