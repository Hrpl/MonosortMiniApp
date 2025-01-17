using Form.Domain.DTO;
using Form.Domain.Response;

namespace Form.API.Services.Interfaces;

public interface IEmailService
{
    public Task<BaseResponseMessage> SendEmail(SendEmailDto data, CancellationToken ct = default);
}
