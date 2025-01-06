using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IDrinkService
{
    public Task<List<GetManyDrinksModel>> GetManyDrinksAsync(int typeId);
}
