using Cepedi.Serasa.Cadastro.Domain.Repositorio.Queries;
using Cepedi.Serasa.Cadastro.Domain.Servicos;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Cepedi.Serasa.Cadastro.Dados.Repositories.Queries.Pessoa;
public class PessoaQueryRepository : BaseDapperRepository, IPessoaQueryRepository
{
    private readonly ICache<List<PessoaEntity>> _cache;

    protected PessoaQueryRepository(IConfiguration configuration, ICache<List<PessoaEntity>> cache) : base(configuration)
    {
        _cache = cache;
    }

    public async Task<List<PessoaEntity>> ObterPessoaPorCpfAsync(string cpf)
    {
        var cacheKey = $"Pessoa:{cpf}";
        var pessoas = await _cache.ObterAsync(cacheKey);

        if (pessoas != default) 
            return pessoas;

        var parametros = new DynamicParameters();
        parametros.Add("@Cpf", cpf, System.Data.DbType.String);

        var sql = @"SELECT
                        Id,
                        Nome,
                        CPF
                    FROM Pessoa WITH(NOLOCK)
                    WHERE
                        CPF = @Cpf";

        var retorno = await ExecuteQueryAsync<PessoaEntity>(sql, parametros);

        await _cache.SalvarAsync(cacheKey, retorno.ToList(), 60 * 5);

        return retorno.ToList();
    }
}
