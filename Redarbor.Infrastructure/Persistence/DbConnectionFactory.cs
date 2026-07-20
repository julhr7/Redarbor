using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Redarbor.Application.Common.Interfaces;

namespace Redarbor.Infrastructure.Persistence;

public class DbConnectionFactory : IDbConnectionFactory 
{
    private readonly string _connectionString;  

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Server=(localdb)\\mssqllocaldb;Database=RedarborDb;Trusted_Connection=True;MultipleActiveResultSets=true";
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}