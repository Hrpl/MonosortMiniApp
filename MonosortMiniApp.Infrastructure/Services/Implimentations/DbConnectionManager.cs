using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MonosortMiniApp.Infrastructure.Services.Interfaces;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Implimentations;

public class DbConnectionManager : IDbConnectionManager
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbConnectionManager> _logger;

    private string dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    private string dbPort = Environment.GetEnvironmentVariable("DB_PORT");
    private string dbUser = Environment.GetEnvironmentVariable("DB_USER");
    private string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
    private string dbName = Environment.GetEnvironmentVariable("DB_CLIENT_NAME");

    private string NpgsqlConnectionString => $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword};";

    public DbConnectionManager(IConfiguration configuration, ILogger<DbConnectionManager> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    private NpgsqlConnection PostgresDbConnection => new(NpgsqlConnectionString);

    public QueryFactory PostgresQueryFactory => new(PostgresDbConnection, new PostgresCompiler(), 60)
    {
        Logger = compiled => { _logger.LogInformation("Query = {@Query}", compiled.ToString()); }
    };
}
