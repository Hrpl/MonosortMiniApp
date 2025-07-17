using Microsoft.AspNetCore.Identity.Data;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IUserService
{
    public Task<bool> IsNewUser(UserAuthRequest request);
    public Task CreateNewUserAsync(UserModel model);
    public string CreateSecretCode(UserAuthRequest request);
    public Task<bool> CheckSecretCode(CheckSecretCodeRequest request);
    public Task<int> GetUserIdAsync(string phone);
}
