using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Entities;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class FavouriteItemService : IFavouriteItemService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "dictionary.FavouriteItem as f";
    public FavouriteItemService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<FavouriteItemModel> GetModel(int userId)
    {
        var query = _query.Query(TableName)
            .Where("f.Id", userId)
            .Select("f.Id",
            "f.UserId",
            "f.Photo",
            "f.DrinkId",
            "f.VolumeId",
            "f.SugarCount",
            "f.SiropId",
            "f.ExtraShot",
            "f.MilkId",
            "f.Sprinkling",
            "f.Price");

        var result = await _query.FirstAsync<FavouriteItemModel>(query);
        return result;
    }

    public async Task CreateFavouriteItemsAsync(FavouriteItemModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        await _query.ExecuteAsync(query);
    }

    public async Task DeleteFavouriteItemsAsync(int id)
    {
        var query = _query.Query(TableName).Where( new { Id = id}).AsDelete();
        await _query.ExecuteAsync(query);
    }

    public async Task<IEnumerable<ProductItemResponse>> GetFavouriteItems(int userId)
    {
        var query = _query.Query("dictionary.FavouriteItem as f")
            .Where("f.UserId", userId)
            .LeftJoin("dictionary.Drinks as d", "f.DrinkId", "d.Id")
            .LeftJoin("dictionary.Volumes as v", "f.VolumeId", "v.Id")
            .LeftJoin("dictionary.Additive as Sirop", join => join.On("f.SiropId", "Sirop.Id"))
            .LeftJoin("dictionary.Additive as Milk", join => join.On("f.MilkId", "Milk.Id"))
            .LeftJoin("dictionary.Additive as Sprinkling", join => join.On("f.Sprinkling", "Sprinkling.Id"))
            .Select(
                "f.Id as Id",
                "d.Name as Drink",
                "d.Photo as Photo",
                "v.Size as Volume",
                "f.SugarCount as SugarCount",
                "f.ExtraShot as ExtraShot",
                "f.Price as Price",
                "Sirop.Name as SiropName",
                "Milk.Name as MilkName",
                "Sprinkling.Name as Sprinkling"
            );

        var result = await _query.GetAsync<ProductItemResponse>(query);
        return result.ToList();
    }

    public async Task<bool> IsContainsAsync(FavouriteItemModel model)
    {
        var query = _query.Query("dictionary.FavouriteItem")
            .Where("UserId", model.UserId)
        .Where("DrinkId", model.DrinkId)
        .Where("VolumeId", model.VolumeId)
        .Where("SugarCount", model.SugarCount)
        .Where("MilkId", model.MilkId)
        .Where("SiropId", model.SiropId)
        .Where("ExtraShot", model.ExtraShot)
        .Where("Sprinkling", model.Sprinkling)
        .Where("Price", model.Price)
        ;

        var exists = await _query.FirstOrDefaultAsync<FavouriteItemModel>(query);
        if (exists == null) return false;
        else return true;
    }
}
