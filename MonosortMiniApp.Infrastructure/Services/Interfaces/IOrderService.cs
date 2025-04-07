using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Entities;
using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    public Task<List<OrderPositionModel>> CreateOrderAsync(OrderModel model);
    public void UpdateStatusAsync(int status, int id);
    public Task<IEnumerable<GetAllOrders>> GetAllOrders(int userId);

}
