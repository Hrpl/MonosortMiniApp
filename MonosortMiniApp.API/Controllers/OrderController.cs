using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;


namespace MonosortMiniApp.API.Controllers
{
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

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<OrderController>
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
}
