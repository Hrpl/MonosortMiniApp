using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementations;

public class ConnectionService : IConnectionService
{
    private readonly QueryFactory _query;
    private string TableName = "public.Connections";
    public ConnectionService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task CreateConnectionEntityAsync(ConnectionModel connection)
    {

        connection.CreatedAt = DateTime.UtcNow;
        connection.IsDeleted = false;

        var query = _query.Query(TableName)
            .AsInsert(connection);

        await _query.ExecuteAsync(query);
    }

    public async Task DeleteConnectionAsync(string connectionId)
    {
        var query = _query.Query(TableName)
            .Where("ConnectionId", connectionId)
            .AsDelete();

        await _query.ExecuteAsync(query);
    }

    public async Task<List<string>> GetAllConnectionsAsync(int? userId)
    {
        var query = _query.Query(TableName)
            .When(userId != null, q => q.Where("UserId", userId))
            .Select("ConnectionId")
            .Where("IsDeleted", false); 

        var connections = await _query.GetAsync<string>(query);

        return connections.ToList();
    }
}
