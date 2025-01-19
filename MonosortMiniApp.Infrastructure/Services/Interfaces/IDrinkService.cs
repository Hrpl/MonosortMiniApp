using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IDrinkService
{
    public Task<List<GetProductsResponse>> GetManyDrinksAsync(int typeId);
    public Task<List<VolumePriceModel>> GetVolumePricesAsync(int id);
    public Task<ProductResponse> GetDrinkAsync(int drinkId);
}
