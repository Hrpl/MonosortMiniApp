using MonosortMiniApp.Domain.Commons.DTO;
using MonosortMiniApp.Domain.Commons.Response;

namespace MonosortMiniApp.API.Services.Interfaces;

public interface IEmailService
{
    public Task<BaseResponseMessage> SendEmail(SendEmailDto data, CancellationToken ct = default);
}
