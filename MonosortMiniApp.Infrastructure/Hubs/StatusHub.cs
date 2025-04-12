
using Microsoft.AspNetCore.SignalR;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Implimentations;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MonosortMiniApp.Infrastructure.Hubs;

public class StatusHub : Hub
{
    private readonly IConnectionService _connectionService;
    private readonly IOrderService _orderService;
    public StatusHub(IConnectionService connectionService, IOrderService orderService)
    {
        _connectionService = connectionService;
        _orderService = orderService;
    }

    public override async Task OnConnectedAsync()
    {
        string? accessToken = Context.GetHttpContext().Request.Query["access_token"];

        var userId = await DecodJwt(accessToken);
        var connect = new ConnectionModel
        {
            UserId = userId,
            ConnectionId = Context.ConnectionId
        };

        await _connectionService.CreateConnectionEntityAsync(connect);

        await this.Clients.Client(Context.ConnectionId).SendAsync("Active", await _orderService.GetAllOrders(userId, true));

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _connectionService.DeleteConnectionAsync(Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }
    //todo
    private static async Task<int> DecodJwt(string? accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new ArgumentException("AccessToken cannot be null or empty", nameof(accessToken));
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);

            var claims = jwtToken.Claims;

            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier || claim.Type == "userId")
                {
                    return Convert.ToInt32(claim.Value);
                }
            }

            throw new InvalidOperationException("UserId not found in the token");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error decoding JWT: {ex.Message}", ex);
        }
    }
}
