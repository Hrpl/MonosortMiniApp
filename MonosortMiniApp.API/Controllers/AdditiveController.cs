using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

namespace MonosortMiniApp.API.Controllers
{
    [Route("api/additive")]
    [ApiController]
    public class AdditiveController : ControllerBase
    {
        private readonly IAdditiveService _additiveService;
        public AdditiveController(IAdditiveService additiveService) 
        {
            _additiveService = additiveService;
        }
        // GET: api/<AdditiveController>
        [HttpGet("many/{id}")]
        public async Task<ActionResult<List<AdditiveModel>>> GetMany([FromRoute]int id)
        {
            var response = await _additiveService.GetManyAdditiveAsync(id);

            if(response != null) return Ok(response);
            else return BadRequest();
        }

    }
}
