using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Domain.Repositorio;

public interface IPessoaRepository
{
    Task<PessoaEntity> ObterPessoaAsync(int id);
    Task<List<PessoaEntity>> ObterPessoasAsync();
    Task<PessoaEntity> CriarPessoaAsync(PessoaEntity pessoa);
    Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa);
    Task ExcluirPessoaAsync(PessoaEntity pessoa);
}
