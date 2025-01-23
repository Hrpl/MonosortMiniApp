using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    public Task CreateOrderAsync(OrderModel model, List<PositionRequest> positions);
    public void UpdateStatusAsync(int status, int id);
    public Task<IEnumerable<GetAllOrders>> GetAllOrders();
}
