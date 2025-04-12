using MonosortMiniApp.Domain.Commons.DTO;
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
    public Task<int> CreateOrderAsync(OrderModel model);
    public void UpdateStatusAsync(int status, int id);
    public Task<IEnumerable<GetAllOrders>> GetAllOrders(int userId);
    public Task<OrderDescriptionResponse> GetOrderItemDescriptionsAsync(int orderId);
    public Task<IEnumerable<StatusOrderDTO>> GetStatusOrder();
    public void UpdateTimeAsync(int time, int orderId);
    public Task<int> GetUserIdOrderAsync(int orderId);
}
