using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonosortMiniApp.API.Services.Interfaces;
using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Templates;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonosortMiniApp.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UserController(IEmailService emailService, IMapper mapper, IUserService userService)
    {
        _emailService = emailService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("confirm")]
    public async Task<IActionResult> Get([FromQuery] string email)
    {
        var user = await _userService.CheckedUserByLoginAsync(email);
        if (user is true)
        {
            await _userService.UserConfirmAsync(email);

            string htmlContent = @"
            <html>
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Redirecting...</title>
                <script type='text/javascript'>
                    window.location.href = 'https://numerology-ai.ru/auth';
                </script>
            </head>
            <body>
                <center>
                    <h1>Ваш email подтверждён</h1>
                </center>
            </body>
            </html>";
            return Content(htmlContent, "text/html");
        }
        else throw new Exception("Неверный email адрес!");
    }

    // POST api/<UserController>
    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] LoginRequest req)
    {
        if (req.Login == "") throw new Exception("Поле Login не заполнено");

        var findEmail = await _userService.CheckedUserByLoginAsync(req.Login);

        if (findEmail is true) throw new Exception("Пользователь с таким email уже существует");

        var user = _mapper.Map<UserModel>(req);

        try
        {
            await _userService.CreatedUserAsync(user);
            try
            {
                var person = new SendEmailDto() { Email = req.Login, Name = "", Subject = "Confirm email", MessageBody = EmailTemplates.RegistrationEmailTemplate.Replace("@email", req.Login) };
                await _emailService.SendEmail(person);
            }
            catch (Exception ex)
            {
                await _userService.DeleteUserAsync(req.Login);
                return BadRequest("Ошибка отправки сообщения пользователю." + ex);
            }
            return Ok(req.Login);
        }
        catch (Exception)
        {
            return BadRequest("Ошибка при создании пользователя");
        }

    }
}
