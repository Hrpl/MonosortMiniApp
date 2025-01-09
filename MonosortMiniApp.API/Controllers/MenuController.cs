using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Implimentations;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using System.ComponentModel.Design;

namespace MonosortMiniApp.API.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IDessertService _dessertService;
        private readonly IMenuService _menuService;
        private readonly IDrinkService _drinkService;
        public MenuController(IDessertService dessertService, IMenuService menuService, IDrinkService drinkService)
        {
            _dessertService = dessertService;
            _menuService = menuService;
            _drinkService = drinkService;
        }

        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<MenuResponse>>> GetCategory()
        {
            var response = await _menuService.GetCategoriesAsync();

            if (response != null) return Ok(response);
            else return BadRequest("Ошибка получения категорий");
        }

        [HttpGet("many/{typeId}")]
        public async Task<ActionResult<IEnumerable<GetProductsResponse>>> GetMany([FromRoute] int typeId)
        {
            var response = new List<GetProductsResponse>();
            if (typeId == 7)
            {
                response = await _dessertService.GetAllAsync();
            }
            else
            {
                response  = await _drinkService.GetManyDrinksAsync(typeId);
            }

            if (response != null) return Ok(response);
            else return BadRequest("Неверный тип");
        }
    }
}
