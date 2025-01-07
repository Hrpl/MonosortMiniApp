using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

namespace MonosortMiniApp.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtHelper _jwtHelper;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IJwtHelper jwtHelper,
        IMapper mapper,
        IUserService userService,
        ILogger<AuthController> logger)
    {
        _jwtHelper = jwtHelper;
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<JwtResponse>> Auth(LoginRequest req)
    {
        var user = await _userService.GetUserAsync(req.Login);
        if (user != null && user.IsConfirmed == false) return BadRequest("Ваш email не подтверждён. Проверьте сообщения на почте.");

        var check = await _userService.LoginUserAsync(req);

        if (check is false) throw new Exception("Неверный логин или пароль");
        var id = await _userService.GetUserIdAsync(req.Login);
        var jwt = _jwtHelper.CreateJwtAsync(id);

        return Ok(jwt);
    }


}
