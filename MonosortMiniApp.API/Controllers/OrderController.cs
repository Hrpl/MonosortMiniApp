using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

    public OrderController(IMapper mapper, IOrderService orderService, ICartService cartService, IHubContext<OrderHub> hubContext, ILogger<OrderController> logger)
    {
        _mapper = mapper;
        _orderService = orderService;
        _cartService = cartService;
        _hubContext = hubContext;
        _logger = logger;
    }

    [HttpGet("all")]
    [Authorize]
    [SwaggerOperation(Summary = "Получени заказов пользователя. Необходим JWT")]
    public async Task<ActionResult<IEnumerable<GetAllOrders>>> GetAll()
    {
        try
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

            var result = await _orderService.GetAllOrders(Convert.ToInt32(userId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ошибка получения заказов: {ex.Message}");
        }
    }

    [HttpPatch("status")]
    public async Task<ActionResult> UpdateStatus([FromQuery] int status, [FromQuery] int id)
    {
        try
        {
            _orderService.UpdateStatusAsync(status, id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Ошибка обновления статуса: {ex.Message}");
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] OrderRequest request)
    {
        try
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

            var orderStatus = await _orderService.CreateOrderAsync(orderModel);
            _logger.LogInformation($"Получены данные: номер {orderStatus.Number}, статус {orderStatus.Status}");
            await _cartService.DeleteAllCart(Convert.ToInt32(userId));

            await _hubContext.Clients.All.SendAsync("SendOrderId", orderStatus);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
