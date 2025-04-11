
using MonosortMiniApp.Domain.Models;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IConnectionService
{
    Task CreateConnectionEntityAsync(ConnectionModel connection);

    Task<List<string>> GetAllConnectionsAsync(int? userId);

    Task DeleteConnectionAsync(string connectionId);
}
