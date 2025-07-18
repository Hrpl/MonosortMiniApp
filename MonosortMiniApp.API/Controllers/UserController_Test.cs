using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.API.Services.Interfaces;
using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Commons.Templates;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MonosortMiniApp.API.Controllers;

[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly ISendSMSService _sendSMS;
    private readonly IJwtHelper _jwtHelper;
    public UserController(IUserService userService, 
        ILogger<UserController> logger, 
        IMapper mapper, 
        ISendSMSService sendSMS,
        IJwtHelper jwtHelper)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
        _sendSMS = sendSMS;
        _jwtHelper = jwtHelper;
    }

    [HttpPost ("checkCode")]
    [SwaggerOperation(Summary = "Проверка кода авторизации.")]
    public async Task<ActionResult> CheckCode([FromBody] CheckSecretCodeRequest request)
    {
        var result = await _userService.CheckSecretCode(request);
        if (!result) return Unauthorized();

        var id = await _userService.GetUserIdAsync(request.PhoneNumber);

        var jwt = _jwtHelper.CreateJwtAsync(id);

        return Ok(jwt);
    }

    [HttpPost("generate/code")]
    [SwaggerOperation(Summary = "Генерация нового кода авторизации.")]
    public async Task<ActionResult> GenerateCode([FromBody] UserAuthRequest request)
    {
        try
        {
            var code = _userService.CreateSecretCode(request);

            _sendSMS.SendSMSAsync(request.PhoneNumber, code);

            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching clients.");
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal server error",
                Detail = $"Произошла ошибка при обработке запроса. \n {ex.Message}"
            });
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Создание нового клиента.")]
    public async Task<ActionResult> Post([FromBody] UserAuthRequest request)
    {
        try
        {
            var isNew = await _userService.IsNewUser(request);
            if (isNew == true)
            {
                var model = _mapper.Map<UserModel>(request);
                await _userService.CreateNewUserAsync(model);
            }

            var code = _userService.CreateSecretCode(request);

            _sendSMS.SendSMSAsync(request.PhoneNumber, code);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching clients.");
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal server error",
                Detail = $"Произошла ошибка при обработке запроса. \n {ex.Message}"
            });
        }

        return Created();
    }

}
