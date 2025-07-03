﻿using Microsoft.IdentityModel.Tokens;
using MonosortMiniApp.Domain.Commons.Response;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class JwtHelper : IJwtHelper
{
    public JwtResponse CreateJwtAsync(int userId)
    {
        var expires = DateTime.UtcNow.AddHours(168);
        var jwt = new JwtSecurityToken(
            issuer: "Server",
            expires: expires,
            claims: new Claim[]
            {
                new ("userId", userId.ToString())
            },
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))), SecurityAlgorithms.HmacSha256)
        );

        var access = new JwtSecurityTokenHandler().WriteToken(jwt);
        var refresh = CreateRefresh();
        return new JwtResponse { AccessToken = access, RefreshToken = refresh, Expires = expires };
    }

    private string CreateRefresh()
    {
        var randomBytes = new byte[64];
        var token = Convert.ToBase64String(randomBytes);
        return token;
    }

    public async Task<int> DecodJwt(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);

            var claims = jwtToken.Payload;
            foreach (var claim in claims)
            {
                if (claim.Key == "userId") return Convert.ToInt32(claim.Value);
            }

            throw new InvalidOperationException("UserId not found in the token");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error decoding JWT: {ex.Message}", ex);
        }
    }
}
