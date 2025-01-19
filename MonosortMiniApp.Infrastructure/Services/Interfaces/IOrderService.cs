using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    public Task CreateOrderAsync(OrderModel model);
    public Task CreatePositionsAsync(List<PositionModel> positionModels);
}
