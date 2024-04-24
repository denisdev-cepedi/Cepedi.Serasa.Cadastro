using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Repositorio;

public interface IConsultaRepository
{
    Task<ConsultaEntity> CriarConsultaAsync(ConsultaEntity Status);
    Task<ConsultaEntity> ObterConsultaAsync(int id);
    Task<ConsultaEntity> AtualizarConsultaAsync(ConsultaEntity Status);
    Task<PessoaEntity> ObterPessoaConsultaAsync(int id);
    Task<ConsultaEntity> DeletarConsultaAsync(int id);
}