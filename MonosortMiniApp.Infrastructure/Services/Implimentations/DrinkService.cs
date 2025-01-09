using MonosortMiniApp.Domain.Commons.Response;
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

    public async Task<List<GetProductsResponse>> GetManyDrinksAsync(int typeId)
    {
        var query = _query.Query(TableName)
            .Where("MenuId", typeId)
            .Where("IsDeleted", false)
            .Select("Id",
            "Name",
            "Photo",
            "IsExistence");

        var result = await _query.GetAsync<GetProductsResponse>(query);

        return result.ToList();
    }

    public async Task<List<VolumePriceModel>> GetVolumePricesAsync(int id)
    {
        var query = _query.Query("dictionary.PriceDrink as pd")
            .Join("dictionary.Volumes as vl", "vl.Id", "pd.VolumeId")
            .Where("pd.DrinkId", id)
            .Select("pd.Price",
            "vl.Name",
            "vl.Size");

        var result = await _query.GetAsync<VolumePriceModel>(query);

        return result.ToList();
    }
}
