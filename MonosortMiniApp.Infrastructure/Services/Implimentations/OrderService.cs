using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class OrderService : IOrderService
{
    private readonly QueryFactory _queryFactory;
    public OrderService(IDbConnectionManager dbConnection)
    {
        _queryFactory = dbConnection.PostgresQueryFactory;
    }
    public Task CreateOrderAsync(OrderModel model)
    {
        throw new NotImplementedException();
    }

    public Task CreatePositionsAsync(List<PositionModel> positionModels)
    {
        throw new NotImplementedException();
    }
}
