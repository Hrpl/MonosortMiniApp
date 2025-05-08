using MonosortMiniApp.Domain.Commons.DTO;
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

public class FavouriteItemService : IFavouriteItemService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "dictionary.FavouriteItem as f";
    public FavouriteItemService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
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

    public async Task<IEnumerable<FavouriteItemDTO>> GetFavouriteItems(int userId)
    {
        var query = _query.Query(TableName)
            .Where("f.UserId", userId)
            .Select("f.Id",
            "f.DrinkId",
            "f.VolumeId",
            "f.SugarCount",
            "f.SiropId",
            "f.ExtraShot",
            "f.MilkId",
            "f.Sprinkling",
            "f.Price");

        var result = await _query.GetAsync<FavouriteItemDTO>(query);
        return result.ToList();
    }
}
