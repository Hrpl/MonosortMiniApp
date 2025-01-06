using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class DictionaryService<T> : IDictionaryService<T>
{
    private readonly QueryFactory _query;
    public DictionaryService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<T> Get(int id, string tableName)
    {
        var query = _query.Query(tableName)
            .Where("Id", id)
            .Select("Id",
            "Price",
            "Name",
            "Photo",
            "IsExistence");

        var response = await _query.FirstAsync<T>(query);
        return response;
    }

    public async Task<List<T>> GetAllAsync(string tableName)
    {
        var query = _query.Query(tableName)
            .Select("Id",
            "Name",
            "Photo",
            "IsExistence");

        var response = await _query.GetAsync<T>(query);
        return response.ToList();
    }
}
