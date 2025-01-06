using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonosortMiniApp.API.Controllers;

[Route("api/sirup")]
[ApiController]
public class SirupController : ControllerBase
{
    private readonly IDictionaryService<SirupModels> _dictionaryService;
    public SirupController(IDictionaryService<SirupModels> dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }
    // GET: api/<SirupController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SirupModels>>> GetMany()
    {
        var result = await _dictionaryService.GetAllAsync("dictionary.Sirups");

        if (result != null) return Ok(result);
        else return BadRequest();
    }
}
