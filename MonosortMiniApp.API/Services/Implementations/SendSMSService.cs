using Newtonsoft.Json;
using System.Text;
using MonosortMiniApp.API.Services.Interfaces;
using MonosortMiniApp.Domain.Commons.Templates;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;

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
            // URL конечной точки API
            var url = "https://api.exolve.ru/messaging/v1/SendSMS";

            // Формируем тело запроса
            var payload = new
            {
                number = "79045661784",// Environment.GetEnvironmentVariable("SMS_NUMBER"), //_configuration["SmsSettings:Phone"],
                destination = phoneNumber,
                text = SMSTemplate.SignMessage.Replace("@code", code)
            };

            // Конвертируем тело запроса в JSON
            var jsonContent = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            // Добавляем заголовок авторизации
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("SMS_TOKEN"));

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
