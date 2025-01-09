using MonosortMiniApp.Domain.Commons.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IMenuService
{
    public Task<List<MenuResponse>> GetCategoriesAsync();
}
