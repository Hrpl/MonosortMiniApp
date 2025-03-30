using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Entities;
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

        int cartId = await _query.FirstOrDefaultAsync<int>(cartIdQuery);
        if(cartId == 0)
        {
            var cartModel = new CartModel() { UserId = userId};
            await this.CreateCartAsync(cartModel);
            cartId = await GetIdCartAsync(userId);
        }

        itemModel.CartId = cartId;

        await _query.Query("dictionary.CartItem").InsertAsync(itemModel);
    }

    public async Task<int> GetIdCartAsync(int userId)
    {
        var cartIdQuery = _query.Query("dictionary.Cart").Where("UserId", userId).Select("Id");

        return await _query.FirstOrDefaultAsync<int>(cartIdQuery);
    }

    public async Task CreateCartAsync(CartModel model)
    {
         await _query.Query("dictionary.Cart").InsertAsync(model);
    }

    public async Task DeleteCartItemAsync(int cartItemId)
    {
        var query = _query.Query("dictionary.CartItem").Where("Id", cartItemId).AsDelete();

        await _query.ExecuteAsync(query);
    }

    public async Task<List<CartItemResponse>> GetCartItemsAsync(int userId)
    {
        var query = _query.Query("dictionary.Cart as c")
            .Where("c.UserId", userId)
            .LeftJoin("dictionary.CartItem as ci", "ci.CartId", "c.Id")
            .LeftJoin("dictionary.Drinks as d", "d.Id", "ci.DrinkId")
            .LeftJoin("dictionary.Volumes as v", "v.Id", "ci.VolumeId")
            .Select("ci.Id",
            "d.Photo",
            "d.Name",
            "v.Size as Volume",
            "ci.Price");

        var result = await _query.GetAsync<CartItemResponse>(query);
        return result.ToList();
    }

    public async Task DeleteAllCart(int userId)
    {
        var cartId = await GetIdCartAsync(userId);
        if (cartId != 0)
        {
            var query = _query.Query("dictionary.CartItem").Where("CartId", cartId).AsDelete();

            await _query.ExecuteAsync(query);
        }
    }
}
