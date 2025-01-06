using Microsoft.AspNetCore.Identity.Data;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IUserService
{
    public Task CreatedUserAsync(UserModel model);
    public Task<bool> CheckedUserByLoginAsync(string login);
    public Task<bool> LoginUserAsync(Domain.Commons.Request.LoginRequest request);
    public Task UserConfirmAsync(string login);
}
