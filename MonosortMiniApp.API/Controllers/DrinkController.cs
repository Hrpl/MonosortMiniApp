using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonosortMiniApp.API.Controllers;

[Route("api/drink")]
[ApiController]
public class DrinkController : ControllerBase
{
    private readonly IDrinkService _drinkService;
    public DrinkController(IDrinkService drinkService)
    {
        _drinkService = drinkService;
    }


    [HttpGet("volume/{id}")]
    [SwaggerOperation(Summary = "Получить информацию об объёмах и ценах за напиток.")]
    public async Task<ActionResult<IEnumerable<VolumePriceModel>>> GetVolume(int id)
    {
        var response = await _drinkService.GetVolumePricesAsync(id);

        if (response != null) return Ok(response);
        else return BadRequest("Ошибка получения объёмов");
    }
}
