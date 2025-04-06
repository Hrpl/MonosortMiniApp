using Microsoft.AspNetCore.SignalR;
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

    public async Task CreateOrderAsync(OrderModel model)
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

            //todo: отправка в тг бота
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<GetAllOrders>> GetAllOrders(int userId)
    {
        var query = _query.Query("dictionary.Orders as o")
            .LeftJoin("dictionary.OrderStatus as os", "os.Id", "o.StatusId")
            .Where("o.UserId", userId)
            .Select("o.Id",
            "os.Status");

        var result = await _query.GetAsync<GetAllOrders>(query);

        return result.ToList();
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
}
