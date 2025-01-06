using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonosortMiniApp.API.Controllers;

[Route("api/milk")]
[ApiController]
public class MilkController : ControllerBase
{
    private readonly IDictionaryService<MilkModel> _dictionaryService;
    public MilkController(IDictionaryService<MilkModel> dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MilkModel>>> GetMany()
    {
        var result = await _dictionaryService.GetAllAsync("dictionary.Milks");

        if(result != null) return Ok(result);
        else return BadRequest();
    }
}
