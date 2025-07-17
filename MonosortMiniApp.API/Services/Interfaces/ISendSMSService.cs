namespace MonosortMiniApp.API.Services.Interfaces;

public interface ISendSMSService
{
    public Task SendSMSAsync(string phoneNumber, string code);
}
