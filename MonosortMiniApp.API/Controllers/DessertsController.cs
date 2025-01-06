using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonosortMiniApp.API.Controllers;

[Route("api/dessert")]
[ApiController]
public class DessertsController : ControllerBase
{
    private readonly IDictionaryService<DessertModel> _dictionaryService;
    public DessertsController(IDictionaryService<DessertModel> dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DessertModel>>> GetMany()
    {
        var result = await _dictionaryService.GetAllAsync("dictionary.Desserts");

        if (result != null) return Ok(result);
        else return BadRequest();
    }
}
