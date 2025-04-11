using Microsoft.OpenApi.Models;
using MonosortMiniApp.API.Extensions;
using MonosortMiniApp.API.Middleware;
using MonosortMiniApp.Infrastructure.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.AddDataBase();
builder.AddJwt();
builder.AddOptionsSmtp();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
    hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(15);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Monosort", Version = "v2024" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization using jwt token. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
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
