using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

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
    public async Task<ActionResult<IEnumerable<VolumePriceModel>>> GetVolume(int id)
    {
        var response = await _drinkService.GetVolumePricesAsync(id);

        if (response != null) return Ok(response);
        else return BadRequest("Ошибка получения объёмов");
    }


}
