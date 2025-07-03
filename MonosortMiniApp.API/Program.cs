using Microsoft.OpenApi.Models;
using MonosortMiniApp.API.Extensions;
using MonosortMiniApp.API.Middleware;
using MonosortMiniApp.Infrastructure.Hubs;
using DotNetEnv;

Env.TraversePath().Load("../.env");

var builder = WebApplication.CreateBuilder(args);
builder.AddDataBase(builder.Configuration);
builder.AddJwt();
builder.AddOptionsSmtp();
builder.AddOpenAPI();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromDays(48); // Почти "бесконечный" таймаут
    options.KeepAliveInterval = TimeSpan.FromDays(25);    // Отправка "живых" пакетов раз в год (фактически отключено)
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddService();
builder.Services.AddCors();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

app.UseMiddleware<ExepctionHandleMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<OrderHub>("/hub/order");
app.MapHub<StatusHub>("/hub/status");

app.Run();
