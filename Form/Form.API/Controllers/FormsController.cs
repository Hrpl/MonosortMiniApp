using Form.API.Services.Interfaces;
using Form.Domain.Domain;
using Form.Domain.DTO;
using Form.Domain.Models;
using Form.Domain.Templates;
using Form.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using SqlKata.Execution;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Form.API.Controllers;

[Route("api/form")]
[ApiController]
public class FormsController : ControllerBase
{
    private readonly QueryFactory _query;
    private readonly IEmailService _emailService;
    public FormsController(IDbConnectionManager connectionManager, IEmailService emailService)
    {
        _query = connectionManager.PostgresQueryFactory;
        _emailService = emailService;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormModels>>> Get()
    {
        var query = _query.Query("Forms")
            .Select("*");

        var result = await _query.GetAsync<FormModels>(query);

        if(result != null) return Ok(result);
        else return BadRequest();
    }


    // POST api/<ValuesController>
    [HttpPost]
    public async void Post([FromBody] FormModels request)
    {
        try
        {
            var query = _query.Query("Forms").Insert(request);
            string drink = "<p>Буду пить</p>";
            string reg = "";
            if (request.BeVodka) drink += "<p>Водка</p>";
            if (request.BeWine) drink += "<p>Вино</p>";
            if (request.BeCongac) drink += "<p>Коньяк</p>";
            if (request.BeChampagne) drink += "<p>Шампанское</p>";
            if (request.DontDrink) drink = "<p>Не буду пить</p>";
            if (request.WillTheRegistration) reg = "<p>Буду на регистрации</p>";
            else reg = "<p>Не буду на регистрации</p>";

            var template = EmailTemplates.Send
                .Replace("@VisitorName", request.VisitorName)
                .Replace("@PhoneNumber", request.PhoneNumber)
                .Replace("@PartnerName", request.PartnerName)
                .Replace("@ChildrenName", request.ChildrenName)
                .Replace("@drink", drink)
                .Replace("@reg", reg);

            var person = new SendEmailDto() { Email = "ponomarevd61@gmail.com", Name = "", Subject = "Notification", MessageBody = template };

            await _emailService.SendEmail(person);

            Ok();
        }
        catch (Exception ex)
        { 
            throw new Exception($"{ex.Message}"); 
        }
    }
}
