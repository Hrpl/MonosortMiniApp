﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MonosortMiniApp.API.Services.Interfaces;
using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Hubs;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MonosortMiniApp.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly IHubContext<OrderHub> _hubContext;
    private readonly ILogger<OrderController> _logger;
    private readonly IHubContext<StatusHub> _hubStatus;
    private readonly IConnectionService _connectionService;

    private readonly IRabbitMq _rabbitMq;

    public OrderController(IMapper mapper,
        IOrderService orderService,
        ICartService cartService,
        IHubContext<OrderHub> hubContext,
        ILogger<OrderController> logger,
        IHubContext<StatusHub> hubStatus,
        IConnectionService connectionService,
        IRabbitMq rabbitMq)
    {
        _mapper = mapper;
        _orderService = orderService;
        _cartService = cartService;
        _hubContext = hubContext;
        _logger = logger;
        _hubStatus = hubStatus;
        _connectionService = connectionService;
        _rabbitMq = rabbitMq;
    }

    [HttpGet("active")]
    [Authorize]
    [SwaggerOperation(Summary = "Получение активного заказа. Необходим JWT")]
    public async Task<ActionResult<LastOrderDTO>> GetActive()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new ProblemDetails
            {
                Title = "Unauthorized",
                Detail = "Invalid user ID in token."
            });
        }

        var result = await _orderService.GetLastActive(Convert.ToInt32(userId));
        if (result == null) return NotFound();
        else return Ok(result);
    }


    [HttpGet("all")]
    [Authorize]
    [SwaggerOperation(Summary = "Получение заказов пользователя для Web. Необходим JWT")]
    public async Task<ActionResult<IEnumerable<GetAllOrders>>> GetAll()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new ProblemDetails
            {
                Title = "Unauthorized",
                Detail = "Invalid user ID in token."
            });
        }

        var result = await _orderService.GetAllOrders(Convert.ToInt32(userId), false);

        var connections = await _connectionService.GetAllConnectionsAsync(Convert.ToInt32(userId));

        foreach (var connection in connections)
        {
            await _hubStatus.Clients.Client(connection).SendAsync("Active", await _orderService.GetAllOrders(Convert.ToInt32(userId), true));
        }

        return Ok(result);
    }

    [HttpGet("description/{orderId}")]
    [SwaggerOperation(Summary = "Получение описания заказа. Необходим JWT")]
    public async Task<ActionResult<IEnumerable<OrderDescriptionResponse>>> GetDescription([FromRoute] int orderId)
    {
        var result = await _orderService.GetOrderItemDescriptionsAsync(Convert.ToInt32(orderId));
        return Ok(result);
    }

    [HttpGet("status")]
    [SwaggerOperation(Summary = "Получение заказов в ТГ Bote. Необходим JWT")]
    public async Task<ActionResult<IEnumerable<StatusOrderDTO>>> GetStatusOrder()
    {
        var result = await _orderService.GetStatusOrder();
        return Ok(result);
    }

    [HttpPatch("status")]
    [SwaggerOperation(Summary = "Изменение статуса заказа")]
    public async Task<ActionResult> UpdateStatus([FromQuery] int status, [FromQuery] int id)
    {
        _orderService.UpdateStatusAsync(status, id);

        if (status != 2)
        {
            var userId = await _orderService.GetUserIdOrderAsync(id);

            var connections = await _connectionService.GetAllConnectionsAsync(userId);

            foreach (var connection in connections)
            {
                var lastActive = await _orderService.GetLastActive(userId);

                if (lastActive != null) await _hubStatus.Clients.Client(connection).SendAsync("Status", lastActive);
                else await _hubStatus.Clients.Client(connection).SendAsync("Status", new LastOrderDTO());

                await _hubStatus.Clients.Client(connection).SendAsync("Active", await _orderService.GetAllOrders(userId, true));
            }
        }

        return NoContent();
    }

    [HttpPatch("waitingTime")]
    [SwaggerOperation(Summary = "Установка времени готовности заказа")]
    public async Task<ActionResult> UpdateWaitingTime([FromQuery] int minuts, [FromQuery] int id)
    {
        _orderService.UpdateTimeAsync(minuts, id);

        var userId = await _orderService.GetUserIdOrderAsync(id);

        var connections = await _connectionService.GetAllConnectionsAsync(userId);

        foreach (var connection in connections)
        {
            var lastActive = await _orderService.GetLastActive(userId);
            if (lastActive != null) await _hubStatus.Clients.Client(connection).SendAsync("Status", lastActive);
            else await _hubStatus.Clients.Client(connection).SendAsync("Status", new LastOrderDTO());

            await _hubStatus.Clients.Client(connection).SendAsync("Active", await _orderService.GetAllOrders(userId, true));
        }

        return NoContent();
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation(Summary = "Создание заказа")]
    public async Task<ActionResult> Post([FromBody] OrderRequest request)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new ProblemDetails
            {
                Title = "Unauthorized",
                Detail = "Invalid user ID in token."
            });
        }

        var orderModel = _mapper.Map<OrderModel>(request);
        orderModel.WaitingTime = 0;
        orderModel.UserId = Convert.ToInt32(userId);
        orderModel.StatusId = 1;

        var orderId = await _orderService.CreateOrderAsync(orderModel);

        await _cartService.DeleteAllCart(Convert.ToInt32(userId));

        var orderDTO = new SendOrderDTO { OrderId = orderId, Date = DateTime.UtcNow };
        //кролик
        _rabbitMq.Serialize(orderDTO);

        var connections = await _connectionService.GetAllConnectionsAsync(Convert.ToInt32(userId));

        foreach (var connection in connections)
        {
            var lastActive = await _orderService.GetLastActive(Convert.ToInt32(userId));
            if (lastActive != null) await _hubStatus.Clients.Client(connection).SendAsync("Status", lastActive);
            else await _hubStatus.Clients.Client(connection).SendAsync("Status", new LastOrderDTO());

            await _hubStatus.Clients.Client(connection).SendAsync("Active", await _orderService.GetAllOrders(Convert.ToInt32(userId), true));
        }

        return Created();
    }
}
