using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Domain.Repositorio.Queries;
public interface IPessoaQueryRepository
{
    Task<List<PessoaEntity>> ObterPessoaPorCpfAsync(string cpf);
}
