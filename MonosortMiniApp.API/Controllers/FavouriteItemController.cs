using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Implimentations;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonosortMiniApp.API.Controllers;

[Route("api/favourite")]
[Authorize]
[ApiController]
public class FavouriteItemController : ControllerBase
{
    private readonly IFavouriteItemService _favouriteItem;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<FavouriteItemController> _logger;

    public FavouriteItemController(IFavouriteItemService favouriteItem, IMapper mapper,
        ILogger<FavouriteItemController> logger, ICartService cartService)
    {
        _favouriteItem = favouriteItem;
        _mapper = mapper;
        _logger = logger;
        _cartService = cartService;
    }

    [HttpPost("add/order")]
    [SwaggerOperation(Summary = "Добавление товара из избранного в корзину. Необходим JWT")]
    public async Task<ActionResult> AddInCart([FromQuery] int id)
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
        var favouriteModel = await _favouriteItem.GetModel(id);

        var model = _mapper.Map<CartItemModel>(favouriteModel);

        await _cartService.CreateCartItemAsync(Convert.ToInt32(userId), model);

        return Created();
    }

    // GET: api/<FavouriteItemController>
    [HttpGet]
    [SwaggerOperation(Summary = "Получить избранные товары для пользователя. Необходим JWT")]
    public async Task<ActionResult<IEnumerable<FavouriteItemDTO>>> Get()
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

        var result = await _favouriteItem.GetFavouriteItems(Convert.ToInt32(userId));
        return Ok(result);
    }

    // POST api/<FavouriteItemController>
    [HttpPost]
    [SwaggerOperation(Summary = "Добавить новый избранный товар. Необходим JWT")]
    public async Task<ActionResult> Post([FromBody] CreateFavouriteItemRequest request)
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
        var model = _mapper.Map<FavouriteItemModel>(request);
        model.UserId = Convert.ToInt32(userId);
        await _favouriteItem.CreateFavouriteItemsAsync(model);

        _logger.LogInformation("Создан новый избранный напиток");

        return Created();
    }

    // DELETE api/<FavouriteItemController>/5
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить избранный товар по id. Необходим JWT")]
    public async Task<ActionResult> Delete(int id)
    {
        await _favouriteItem.DeleteFavouriteItemsAsync(id);

        _logger.LogInformation($"Удалён избранный напиток с id: {id}");

        return NoContent();
    }
}
