using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Implimentations;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MonosortMiniApp.API.Controllers
{
    [Route("api/additive")]
    [ApiController]
    public class AdditiveController : ControllerBase
    {
        private readonly IAdditiveService _additiveService;
        private readonly IDrinkService _drinkService;

        public AdditiveController(IAdditiveService additiveService, IDrinkService drinkService) 
        {
            _additiveService = additiveService;
            _drinkService = drinkService;
        }

        // GET: api/<AdditiveController>
        [HttpGet("many/{id}")]
        [SwaggerOperation(Summary = "Получение списка добавок из категории")]
        public async Task<ActionResult<List<AdditiveModel>>> GetMany([FromRoute]int id)
        {
            var response = await _additiveService.GetManyAdditiveAsync(id);

            if(response != null) return Ok(response);
            else return BadRequest();
        }

        [HttpGet("type")]
        [SwaggerOperation(Summary = "Получение категорий добавок")]
        public async Task<ActionResult<List<AdditiveModel>>> GetTypeAdditive([FromQuery] int drinkId)
        {

            var response = await _additiveService.GetTypeAdditiveAsync(drinkId);

            if (response != null) return Ok(response);
            else return BadRequest();
        }

    }
}
