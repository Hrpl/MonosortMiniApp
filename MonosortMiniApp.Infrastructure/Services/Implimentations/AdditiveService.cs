﻿using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class AdditiveService : IAdditiveService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "dictionary.Additive";
    public AdditiveService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }
    public async Task<List<AdditiveModel>> GetManyAdditiveAsync(int typeId)
    {
        var query = _query.Query(TableName)
            .Where("TypeAdditiveId", typeId)
            .Select("Id",
            "Name",
            "Price",
            "Photo",
            "IsExistence");

        var result = await _query.GetAsync<AdditiveModel>(query);
        return result.ToList();
    }

    public async Task<List<GetTypeAdditive>> GetTypeAdditiveAsync()
    {
        var query = _query.Query("dictionary.TypeAdditive")
            .Select("Id",
            "Name");

        var result = await _query.GetAsync<GetTypeAdditive>(query);
        return result.ToList();
    }
}
