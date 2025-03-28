using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

namespace MonosortMiniApp.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly IJwtHelper _jwtHelper;

    public OrderController(IMapper mapper, IOrderService orderService, IJwtHelper jwtHelper)
    {
        _mapper = mapper;
        _orderService = orderService;
        _jwtHelper = jwtHelper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<GetAllOrders>>> GetAll()
    {
        try
        {
            var result = await _orderService.GetAllOrders();
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
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var id = await _jwtHelper.DecodJwt(token);


                var order = _mapper.Map<OrderModel>(request);
                order.WaitingTime = 0;
                order.UserId = id;
                order.StatusId = 1;

                await _orderService.CreateOrderAsync(order, request.Positions);
                return Ok();
            }
            else return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
