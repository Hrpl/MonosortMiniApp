﻿using Microsoft.AspNetCore.SignalR;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Hubs;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using IMapper = MapsterMapper.IMapper;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class OrderService : IOrderService
{
    private readonly QueryFactory _query;
    private readonly IMapper _mapper;
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderService(IDbConnectionManager query, IMapper mapper, IHubContext<OrderHub> hubContext)
    {
        _query = query.PostgresQueryFactory;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public async Task CreateOrderAsync(OrderModel model, List<PositionRequest> positions)
    {
        try
        {
            var qid = _query.Query("dictionary.Orders")
            .InsertGetId<int>(model);
            if(qid != 0)
            {
                foreach (var position in positions)
                {
                    var positionModel = _mapper.Map<PositionModel>(position);

                    positionModel.OrderId = qid;
                    var pid = _query.Query("dictionary.OrderItem").InsertGetId<int>(positionModel);

                    foreach (var sirop in position.Sirops)
                    {
                        var siropPosition = new SiropPositionModel { SiropId = sirop, OrderItemId = pid };
                        await _query.Query("dictionary.SiropsPosition").InsertAsync(siropPosition);
                    }
                }
            }
            else
            {
                //todo
                throw new Exception("Ошибка в создании заказа");
            }

            await _hubContext.Clients.All.SendAsync("NewOrder", qid);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    public async Task<IEnumerable<GetAllOrders>> GetAllOrders()
    {
        var query = _query.Query("dictionary.Orders")
            .Select("Id",
            "CreatedAt");

        var result = await _query.GetAsync<GetAllOrders>(query);

        return result.ToList();
    }

    public void UpdateStatusAsync(int status, int id)
    {
        _query.Query("dictionary.Orders").Where("Id", id).Update(new
        {
            StatusId = status
        });
    }
}
