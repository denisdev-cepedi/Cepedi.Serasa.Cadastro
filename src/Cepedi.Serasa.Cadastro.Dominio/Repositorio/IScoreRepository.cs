using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Repositorio;

public interface IScoreRepository
{
    Task<ScoreEntity> CriarScoreAsync(ScoreEntity score);
    Task<ScoreEntity> ObterScoreAsync(int id);
    Task<ScoreEntity> AtualizarScoreAsync(ScoreEntity score);
    Task<PessoaEntity> ObterPessoaScoreAsync(int id);
    Task<ScoreEntity> DeletarScoreAsync(int id);
}
