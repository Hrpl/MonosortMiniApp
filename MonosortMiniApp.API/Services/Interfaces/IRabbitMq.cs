namespace MonosortMiniApp.API.Services.Interfaces;

public interface IRabbitMq
{
    public void Serialize(object obj);
    public Task SendOrder(string message);

}
