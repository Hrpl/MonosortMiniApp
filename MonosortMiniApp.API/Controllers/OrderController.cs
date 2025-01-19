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

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<OrderController>
        [HttpPost]
        public async void Post([FromBody] OrderRequest request)
        {
            try
            {
                var order = _mapper.Map<OrderModel>(request);

                await _orderService.CreateOrderAsync(order);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
