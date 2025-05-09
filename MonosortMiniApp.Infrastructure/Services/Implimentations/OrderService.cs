﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Entities;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Hubs;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata;
using SqlKata.Execution;
using System;
using IMapper = MapsterMapper.IMapper;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class OrderService : IOrderService
{
    private readonly QueryFactory _query;
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;
    TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");

    public OrderService(IDbConnectionManager query, IMapper mapper, ILogger<OrderService> logger)
    {
        _query = query.PostgresQueryFactory;
        _mapper = mapper;
        _logger = logger;
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

    public async Task<IEnumerable<GetAllOrders>> GetAllOrders(int userId, bool onlyActive)
    {
        var ordersQuery = _query.Query("dictionary.Orders as o")
        .LeftJoin("dictionary.OrderStatus as os", "o.StatusId", "os.Id")
        .Where(q => q
                .Where("o.UserId", userId)
                .When(onlyActive, q => q.WhereIn("o.StatusId", new[] { 1, 2, 3 }), q => q.Where("o.StatusId", 4))
            )
        .Select(
            "o.Id as OrderId",
            "os.Name as Status",
            "o.SummaryPrice as SummaryPrice",
            "o.CreatedAt as CreatedTime",
            "o.WaitingTime as WaitingTime",
            "o.UpdatedAt as UpdatedAt"
        )
        .OrderByDesc("o.CreatedAt")
        .Take(10);

        var ordersResponse = await _query.GetAsync<GetAllOrdersModel>(ordersQuery);

        if (ordersResponse == null || !ordersResponse.Any())
            return new List<GetAllOrders>();

        foreach (var order in ordersResponse)
        {
            if (order.WaitingTime > 0 && order.Status == "Готовится" && order.UpdatedAt != null)
            {
                _logger.LogInformation($"Время UpdatedAt: {order.UpdatedAt}, WaitingTime: {order.WaitingTime}");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(order.UpdatedAt.Value, timeZone);
                order.ReadyTime = localTime.AddMinutes(order.WaitingTime).ToString("HH:mm");

                _logger.LogInformation($"Время готовности: {order.ReadyTime}");
            }
            else
            {
                order.ReadyTime = null;
            }
        }
        List<GetAllOrders> orders = new List<GetAllOrders>();

        foreach (var order in ordersResponse)
        {
            var orderMap = _mapper.Map<GetAllOrders>(order);
            orders.Add(orderMap);
        }
        

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
        if (status == 2)
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
            .Where(q => q
                .Where("o.CreatedAt", ">=", DateTime.Now.AddDays(-1))
                .WhereIn("o.StatusId", new[] { 1, 2, 3 })
            )
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

    public async Task<int> GetUserIdOrderAsync(int orderId)
    {
        var query = _query.Query("dictionary.Orders as o")
            .Where("o.Id", orderId)
            .Select("o.UserId");

        var result = await _query.FirstOrDefaultAsync<int>(query);

        return result;
    }

    public async Task<LastOrderDTO?> GetLastActive(int userId)
    {
        var query = _query.Query("dictionary.Orders as o")
            .Where(q => q
                .Where("o.CreatedAt", ">=", DateTime.Now.AddDays(-1))
                .Where("o.UserId", userId)
                .WhereIn("o.StatusId", new[] { 1, 2, 3 })
            )
            .LeftJoin("dictionary.OrderStatus as os", "os.Id", "o.StatusId")
            .Select("o.Id as Number",
            "o.SummaryPrice as Price",
            "os.Name as Status",
            "o.WaitingTime as WaitingTime",
            "o.UpdatedAt as UpdatedAt")
            .OrderByDesc("o.CreatedAt");

        var model = await _query.FirstOrDefaultAsync<LastOrderModel>(query);
        if (model == null) return null;
        var result = _mapper.Map<LastOrderDTO>(model);

        if (model.Status == "Готовится" && model.UpdatedAt != null)
        {
            _logger.LogInformation($"Время UpdatedAt: {model.UpdatedAt}, WaitingTime: {model.WaitingTime}");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(model.UpdatedAt.Value, timeZone);
            result.ReadyTime = localTime.AddMinutes(model.WaitingTime).ToString("HH:mm");
            _logger.LogInformation($"Время готовности: {result.ReadyTime}");
        }

        return result;
    }
}
