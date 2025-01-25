using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class CartService : ICartService
{
    private readonly QueryFactory _query;
    public CartService(IDbConnectionManager dbConnection)
    {
        _query = dbConnection.PostgresQueryFactory;
    }

    public async Task CreateCartItemAsync(int userId, CartItemModel itemModel)
    {
        var cartIdQuery = _query.Query("dictionary.Cart")
            .Where("UserId", userId)
            .Select("Id");

        var cartId = await _query.FirstOrDefaultAsync<int>(cartIdQuery);
        if(cartId == 0)
        {
            var cartModel = new CartModel() { UserId = userId};
            await this.CreateCartAsync(cartModel);
        }

        itemModel.CartId = cartId;

        await _query.Query("dictionary.CartItem").InsertAsync(itemModel);
    }

    public async Task CreateCartAsync(CartModel model)
    {
         await _query.Query("dictionary.Cart").InsertAsync(model);
    }

    public async Task DeleteCartItemAsync(int userId, int cartItemId)
    {
        var query = _query.Query("dictionary.CartItem").Where("Id", cartItemId).AsDelete();

        await _query.ExecuteAsync(query);
    }

    public async Task<List<CartItemResponse>> GetCartItemsAsync(int userId)
    {
        var query = _query.Query("dictionary.Cart as c")
            .Where("c.UserId", userId)
            .Join("dictionary.CartItem as ci", "ci.CartId", "c.Id")
            .Select("ci.Id",
            "ci.DrinkId",
            "ci.VolumeId",
            "ci.SugarCount",
            "ci.MilkId",
            "ci.ExtraShot",
            "ci.Price");

        var result = await _query.GetAsync<CartItemResponse>(query);
        return result.ToList();
    }
}
