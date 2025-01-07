using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class UserTgService : IUserTgService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "public.TelegramUser";
    public UserTgService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }
    public async Task CreateUserTg(UserTgModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        await _query.ExecuteAsync(query);
    }
}
