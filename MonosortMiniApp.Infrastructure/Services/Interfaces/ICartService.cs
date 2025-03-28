using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface ICartService
{
    public Task CreateCartItemAsync(int userId, CartItemModel itemModel);
    public Task<List<CartItemResponse>> GetCartItemsAsync(int userId);
    public Task DeleteCartItemAsync(int cartItemId);
    public Task DeleteAllCart(int userId);
}
