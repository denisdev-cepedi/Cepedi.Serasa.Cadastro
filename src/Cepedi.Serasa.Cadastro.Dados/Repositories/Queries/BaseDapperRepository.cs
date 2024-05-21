using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Cepedi.Serasa.Cadastro.Dados.Repositories.Queries;

public class BaseDapperRepository
{
    private readonly string? _connectionString;

    protected BaseDapperRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public virtual async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, DynamicParameters parameters)
    {
        using var conn = GetConnection();

        conn.Open();

        var result = await conn.QueryAsync<T>(query, parameters);

        conn.Close();

        return result.ToList();
    }

    private IDbConnection GetConnection()
        => new SqlConnection(_connectionString);
}
