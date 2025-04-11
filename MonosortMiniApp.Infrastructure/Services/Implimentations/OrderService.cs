using Microsoft.AspNetCore.SignalR;
using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Entities;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Hubs;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using IMapper = MapsterMapper.IMapper;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class OrderService : IOrderService
{
    private readonly QueryFactory _query;

    public OrderService(IDbConnectionManager query)
    {
        _query = query.PostgresQueryFactory;
    }

    public async Task<int> CreateOrderAsync(OrderModel model)
    {
        try
        {
            var cartItems = await GetUserCartItemsAsync(model.UserId);

            var qid = _query.Query("dictionary.Orders")
            .InsertGetId<int>(model);

            foreach (var position in cartItems)
            {
                position.OrderId = qid;
            }

            await CreateOrderPositions(cartItems.ToList());

            return qid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<GetAllOrders>> GetAllOrders(int userId)
    {
        var ordersQuery = _query.Query("dictionary.Orders as o")
        .LeftJoin("dictionary.OrderStatus as os", "o.StatusId", "os.Id")
        .Where("o.UserId", userId)
        .Select(
            "o.Id as OrderId",
            "os.Name as Status",
            "o.SummaryPrice as SummaryPrice",
            "o.CreatedAt as CreatedTime"
        )
        .OrderByDesc("o.CreatedAt")
        .Take(10);

        var orders = await _query.GetAsync<GetAllOrders>(ordersQuery);

        if (orders == null || !orders.Any())
            return new List<GetAllOrders>();

        // Получаем все ID заказов для выборки позиций
        var orderIds = orders.Select(o => o.OrderId).ToList();

        // Запрос для получения всех позиций этих заказов
        var itemsQuery = _query.Query("dictionary.OrderItems as oi")
            .WhereIn("oi.OrderId", orderIds)
            .LeftJoin("dictionary.Drinks as d", "oi.DrinkId", "d.Id")
            .LeftJoin("dictionary.Volumes as v", "oi.VolumeId", "v.Id")
            .LeftJoin("dictionary.Additive as Sirop", join => join.On("oi.SiropId", "Sirop.Id"))
            .LeftJoin("dictionary.Additive as Milk", join => join.On("oi.MilkId", "Milk.Id"))
            .LeftJoin("dictionary.Additive as Sprinkling", join => join.On("oi.Sprinkling", "Sprinkling.Id"))
            .Select(
                "oi.OrderId as OrderId",
                "d.Name as DrinkName",
                "d.Photo as Photo",
                "v.Size as VolumeName",
                "oi.SugarCount as SugarCount",
                "oi.ExtraShot as ExtraShot",
                "oi.Price as Price",
                "Sirop.Name as SiropName",
                "Milk.Name as MilkName",
                "Sprinkling.Name as Sprinkling"
            );

        var orderItems = await _query.GetAsync<OrderItemDescriptionDTO>(itemsQuery);

        // Группируем позиции по заказам
        var itemsGroupedByOrder = orderItems
            .GroupBy(item => item.OrderId)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Связываем заказы с их позициями
        foreach (var order in orders)
        {
            if (itemsGroupedByOrder.TryGetValue(order.OrderId, out var items))
            {
                order.OrderItems = items;
            }
            else
            {
                order.OrderItems = new List<OrderItemDescriptionDTO>();
            }
        }

        return orders;
    }

    public void UpdateStatusAsync(int status, int id)
    {
        if(status == 2) 
        {
            _query.Query("dictionary.Orders").Where("Id", id).Update(new
            {
                StatusId = status,
                UpdatedAt = DateTime.UtcNow
            });
        }
        else
        {
            _query.Query("dictionary.Orders").Where("Id", id).Update(new
            {
                StatusId = status
            });
        }
    }

    private async Task CreateOrderPositions(List<OrderPositionModel> models)
    {
        foreach (var model in models)
        {
             _query.Query("dictionary.OrderItems").Insert(model);
        }
    }

    private async Task<IEnumerable<OrderPositionModel>> GetUserCartItemsAsync(int userId)
    {
        var cartItemsQuery = _query.Query("dictionary.Cart as c")
                .Join("dictionary.CartItem as ci", "ci.CartId", "c.Id")
                .Where("c.UserId", userId)
                .Select("ci.DrinkId",
                "ci.VolumeId",
                "ci.SugarCount",
                "ci.SiropId",
                "ci.MilkId",
                "ci.ExtraShot",
                "ci.Price",
                "ci.Sprinkling");

        var cartItems = await _query.GetAsync<OrderPositionModel>(cartItemsQuery);

        return cartItems;
    }

    public async Task<IEnumerable<StatusOrderDTO>> GetStatusOrder()
    {
        var query = _query.Query("dictionary.Orders as o")
            .Where("o.CreatedAt", ">=", DateTime.Now.AddDays(-1))
            .LeftJoin("dictionary.OrderStatus as os", "os.Id", "o.StatusId")
            .Select("o.Id as Number",
            "o.SummaryPrice as Price",
            "os.Name as Status");

        var result = await _query.GetAsync<StatusOrderDTO>(query);

        return result;
    }

    public async Task<OrderDescriptionResponse> GetOrderItemDescriptionsAsync(int orderId)
    {
        var query = _query.Query("dictionary.Orders as o")
        .LeftJoin("dictionary.OrderStatus as os", "o.StatusId", "os.Id")
        .Where("o.Id", orderId)
        .Select(
            "o.WaitingTime as WaitingTime",
            "os.Name as Status",
            "o.SummaryPrice as SummaryPrice",
            "o.Comment as Comment",
            "o.CreatedAt as CreatedTime"
        );

        var orderDescription = await _query.FirstOrDefaultAsync<OrderDescriptionResponse>(query);

        if (orderDescription == null)
            return null;

        var itemsQuery = _query.Query("dictionary.OrderItems as oi")
            .Where("oi.OrderId", orderId)
            .LeftJoin("dictionary.Drinks as d", "oi.DrinkId", "d.Id")
            .LeftJoin("dictionary.Volumes as v", "oi.VolumeId", "v.Id")
            .LeftJoin("dictionary.Additive as Sirop", join => join.On("oi.SiropId", "Sirop.Id"))
            .LeftJoin("dictionary.Additive as Milk", join => join.On("oi.MilkId", "Milk.Id"))
            .LeftJoin("dictionary.Additive as Sprinkling", join => join.On("oi.Sprinkling", "Sprinkling.Id"))
            .Select(
                "d.Name as DrinkName",
                "v.Size as VolumeName",
                "oi.SugarCount as SugarCount",
                "oi.ExtraShot as ExtraShot",
                "oi.Price as Price",
                "Sirop.Name as SiropName",
                "Milk.Name as MilkName",
                "Sprinkling.Name as Sprinkling"
            );

        orderDescription.OrderItems = await _query.GetAsync<OrderItemDescriptionDTO>(itemsQuery);

        return orderDescription;
    }

    public void UpdateTimeAsync(int time, int orderId)
    {
        _query.Query("dictionary.Orders").Where("Id", orderId).Update(new
        {
            WaitingTime = time,
            UpdatedAt = DateTime.UtcNow
        });
    }
}
