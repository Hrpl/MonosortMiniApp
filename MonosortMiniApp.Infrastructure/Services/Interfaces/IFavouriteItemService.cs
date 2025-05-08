using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Models;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IFavouriteItemService
{
    public Task<IEnumerable<FavouriteItemDTO>> GetFavouriteItems(int userId);
    public Task CreateFavouriteItemsAsync(FavouriteItemModel model);
    public Task DeleteFavouriteItemsAsync(int id);
}
