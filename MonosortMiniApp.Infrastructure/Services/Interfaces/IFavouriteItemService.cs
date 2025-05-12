using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IFavouriteItemService
{
    public Task<IEnumerable<ProductItemResponse>> GetFavouriteItems(int userId);
    public Task CreateFavouriteItemsAsync(FavouriteItemModel model);
    public Task DeleteFavouriteItemsAsync(int id);
    public Task<FavouriteItemModel> GetModel(int userId);
    public Task<bool> IsContainsAsync(FavouriteItemModel model);
}
