using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IAdditiveService
{
    public Task<List<AdditiveModel>> GetManyAdditiveAsync(int typeId);

    public Task<List<GetTypeAdditive>> GetTypeAdditiveAsync(int drinkId);
}
