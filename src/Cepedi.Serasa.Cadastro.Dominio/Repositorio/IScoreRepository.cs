using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Repositorio;

public interface IScoreRepository
{
    Task<ScoreEntity> CriarScoreAsync(ScoreEntity score);
    Task<ScoreEntity> ObterScoreAsync(int id);
    Task<List<ScoreEntity>> ObterTodosScoresAsync();
    Task<ScoreEntity> AtualizarScoreAsync(ScoreEntity score);
    Task<ScoreEntity> ObterPessoaScoreAsync(int id);
    Task<ScoreEntity> DeletarScoreAsync(int id);
}
