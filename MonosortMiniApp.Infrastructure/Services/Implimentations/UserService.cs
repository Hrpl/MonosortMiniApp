﻿
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Models;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using SqlKata;
using SqlKata.Execution;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class UserService : IUserService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "dictionary.User";
    public UserService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<bool> CheckedUserByLoginAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("Login", login)
            .Select("Login");

        var result = await _query.FirstOrDefaultAsync<string>(query);

        if (result != null) return true;
        else return false;
    }

    public async Task CreatedUserAsync(UserModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        await _query.ExecuteAsync(query);
    }

    public async Task DeleteUserAsync(string login)
    {
        var query = _query.Query(TableName).Where("Login", login).AsDelete();

        await _query.ExecuteAsync(query);
    }

    public Task<UserModel> GetUserAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("Login", login)
            .Select("Login",
            "Password",
            "IsConfirmed",
            "CreatedAt",
            "UpdatedAt");

        var result = _query.FirstOrDefaultAsync<UserModel>(query);
        return result;
    }

    public async Task<int> GetUserIdAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("Login", login)
            .Select("Id");

        var result = await _query.FirstAsync<int>(query);
        return result;
    }

    public async Task<bool> LoginUserAsync(LoginRequest request)
    {
        var query = _query.Query(TableName)
            .Where("Login", request.Login)
            .Where("Password", request.Password)
            .Select("Login");

        var result = await _query.FirstOrDefaultAsync<string>(query);

        if (result != null) return true;
        else return false;
    }

    public async Task UserConfirmAsync(string login)
    {
        var query = _query.Query(TableName).Where("Login", login).AsUpdate(new
        {
            IsConfirmed = true
        });

        await _query.ExecuteAsync(query);
    }
}
