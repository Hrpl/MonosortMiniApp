using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonosortMiniApp.API.Controllers;

[Route("api/cart")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IJwtHelper _jwtHelper;
    private readonly IMapper _mapper;
    public CartController(ICartService cartService, IMapper mapper, IJwtHelper jwtHelper)
    {
        _cartService = cartService;
        _mapper = mapper;
        _jwtHelper = jwtHelper;
    }

    // GET: api/<CartController>
    [HttpGet("all")]
    [SwaggerOperation(Summary = "Получает все товары в корзине. Необходим JWT")]
    public async Task<ActionResult<IEnumerable<CartItemResponse>>> Get()
    {
        try
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var id = await _jwtHelper.DecodJwt(token);

                var result = await _cartService.GetCartItemsAsync(id);
                return Ok(result);

            }
            else return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Добавление товара в корзину. Необходим JWT")]
    public async Task<ActionResult> Post([FromBody] CartItemRequest request)
    {
        try
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var id = await _jwtHelper.DecodJwt(token);

                var model = _mapper.Map<CartItemModel>(request);

                await _cartService.CreateCartItemAsync(id, model);

                return Created();

            }
            else return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/<CartController>/5
    [HttpDelete("{cartItemId}")]
    [SwaggerOperation(Summary = "Удаление товара из корзины. Необходим JWT")]
    public async Task<ActionResult> Delete(int cartItemId)
    {
        try
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                await _cartService.DeleteCartItemAsync(cartItemId);
                return Ok();
            }
            else return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("all")]
    [SwaggerOperation(Summary = "Удаление всех товаров из корзины. Необходим JWT")]
    public async Task<ActionResult> DeleteAll()
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

            await _cartService.DeleteAllCart(Convert.ToInt32(userId));
            return Ok();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
