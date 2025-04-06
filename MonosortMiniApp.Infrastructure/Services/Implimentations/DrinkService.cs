using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Npgsql.Internal.Postgres;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonosortMiniApp.Domain.Constant.EntityInformation;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class DrinkService : IDrinkService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "dictionary.Drinks as d";
    public DrinkService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<ProductResponse> GetDrinkAsync(int drinkId)
    {
        var querydDrinks = _query.Query("dictionary.Drinks as d")
            .Where("d.Id", drinkId)
            .Where("d.IsDeleted", false)
            .Select("d.Id",
            "d.Name",
            "d.Photo",
            "d.IsExistence");

        var response = await _query.FirstAsync<ProductResponse>(querydDrinks);

        var queryVolumes = _query.Query("dictionary.PriceDrink as pd")
            .Join("dictionary.Volumes as v", "v.Id", "pd.VolumeId")
            .Where("DrinkId", drinkId)
            .Select("pd.Price",
            "v.Name",
            "v.Size");
        var volumes = await _query.GetAsync<VolumePriceModel>(queryVolumes);

        response.VolumePriceModels = volumes.ToList();

        return response;
    }

    public async Task<List<GetProductsResponse>> GetManyDrinksAsync(int typeId)
    {
        var query = _query.Query("dictionary.Drinks as d")
            .Where("d.MenuId", typeId)
            .Where("d.IsDeleted", false)
            .Select("d.Id",
            "d.Name",
            "d.Photo",
            "d.IsExistence");

        var result = await _query.GetAsync<GetProductsResponse>(query);

        foreach (var product in result) { 
            query = _query.Query("dictionary.PriceDrink")
                .Where("DrinkId", product.Id)
                .Select("Price")
                .OrderBy("Price")
                .Limit(1);

            var price = await _query.FirstAsync<int>(query);
            product.Price = price;
        }

        return result.ToList();
    }

    public async Task<List<VolumePriceModel>> GetVolumePricesAsync(int id)
    {
        var query = _query.Query("dictionary.PriceDrink as pd")
            .Join("dictionary.Volumes as vl", "vl.Id", "pd.VolumeId")
            .Where("pd.DrinkId", id)
            .Select("pd.Price",
            "vl.Id as VolumeId",
            "vl.Name",
            "vl.Size");
        
        var result = await _query.GetAsync<VolumePriceModel>(query);

        return result.ToList();
    }
}
