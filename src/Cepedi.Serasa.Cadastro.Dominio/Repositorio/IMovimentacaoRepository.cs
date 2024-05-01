using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Repositorio;

public interface IMovimentacaoRepository
{
    Task<MovimentacaoEntity> ObterMovimentacaoAsync(int id);
    Task<List<MovimentacaoEntity>> ObterMovimentacoesAsync();
    Task<MovimentacaoEntity> CriarMovimentacaoAsync(MovimentacaoEntity movimentacao);
    Task<MovimentacaoEntity> AtualizarMovimentacaoAsync(MovimentacaoEntity movimentacao);
    Task DeletarMovimentacaoAsync(MovimentacaoEntity movimentacao);
}
