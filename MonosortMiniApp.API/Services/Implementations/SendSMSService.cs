using Newtonsoft.Json;
using System.Text;
using MonosortMiniApp.API.Services.Interfaces;
using MonosortMiniApp.Domain.Commons.Templates;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MonosortMiniApp.API.Services.Implementations;

public class SendSMSService : ISendSMSService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public SendSMSService(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
    }
    public async Task SendSMSAsync(string phoneNumber, string code)
    {
        try
        {
            var text = SMSTemplate.SignMessage.Replace("@code", code);
            var smsToken = Environment.GetEnvironmentVariable("SMS_TOKEN");
            // URL конечной точки API
            var url = $"https://sms.ru/sms/send?api_id={smsToken}&to={phoneNumber}&msg={text}&json=1";
            

            // Конвертируем тело запроса в JSON
            var jsonContent = JsonConvert.SerializeObject("");
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Выполняем POST-запрос
            var response = await _httpClient.PostAsync(url, httpContent);

            // Обрабатываем ответ
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Сообщение отправлено успешно.");
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            else
            {

                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка: {response.StatusCode} {errorContent}");
                throw new Exception($"Ответ: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
