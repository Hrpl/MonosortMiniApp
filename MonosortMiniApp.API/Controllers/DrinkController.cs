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

    [HttpGet("many/{typeId}")]
    public async Task<ActionResult<IEnumerable<GetManyDrinksModel>>> GetMany([FromRoute] int typeId)
    {
        var response = await _drinkService.GetManyDrinksAsync(typeId);

        if(response != null) return Ok(response);
        else return BadRequest("Неверный тип");
    }

    [HttpGet("category")]
    public async Task<ActionResult<IEnumerable<DrinkCategoryResponse>>> GetCategory()
    {
        var response = await _drinkService.GetDrinkCategoriesAsync();

        if (response != null) return Ok(response);
        else return BadRequest("Ошибка получения категорий");
    }


    
}
