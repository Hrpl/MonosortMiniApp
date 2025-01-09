using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class DessertService : IDessertService
{
    private readonly QueryFactory _query;
    private string TableName = "dictionary.Desserts";
    public DessertService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<List<GetProductsResponse>> GetAllAsync()
    {
        var query = _query.Query(TableName)
            .Select("Id",
            "Name",
            "Photo",
            "IsExistence");

        var response = await _query.GetAsync<GetProductsResponse>(query);
        return response.ToList();
    }
}
