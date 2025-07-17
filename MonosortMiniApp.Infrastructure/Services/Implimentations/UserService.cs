
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
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

    public async Task<bool> CheckSecretCode(CheckSecretCodeRequest request)
    {
        var query = _query.Query(TableName)
            .Where("PhoneNumber", request.PhoneNumber)
            .Where("Code", request.Code)
            .Select("PhoneNumber");

        var result = await _query.FirstOrDefaultAsync<string>(query);

        if (result == request.PhoneNumber) return true;
        else return false;
    }

    public async Task CreateNewUserAsync(UserModel model)
    {
        var query = _query.Query(TableName)
            .AsInsert(model);

        await _query.ExecuteAsync(query);
    }

    public string CreateSecretCode(UserAuthRequest request)
    {
        Random rand = new Random();
        string code = Convert.ToString(rand.Next(1000, 9999));

        var query = _query.Query(TableName)
            .Where("PhoneNumber", request.PhoneNumber)
            .Update(new
            {
                Code = code
            });

        return code;
    }

    public async Task<int> GetUserIdAsync(string phone)
    {
        var query = _query.Query(TableName)
            .Where("PhoneNumber", phone)
            .Select("Id");

        var result = await _query.FirstOrDefaultAsync<int?>(query);
        return result ?? 0;
    }

    public async Task<bool> IsNewUser(UserAuthRequest request)
    {
        var query = _query.Query(TableName)
            .Where("PhoneNumber", request.PhoneNumber);

        var response = await _query.FirstOrDefaultAsync<UserModel>(query);

        if (response == null) return true;
        else return false;
    }
}
