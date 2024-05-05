using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Repositorio;

public interface IMovimentacaoRepository
{   
    Task<List<MovimentacaoEntity>> ObterTodasMovimentacoesAsync();
    Task<MovimentacaoEntity> ObterMovimentacaoAsync(int id);
    Task<MovimentacaoEntity> CriarMovimentacaoAsync(MovimentacaoEntity movimentacao);
    Task<MovimentacaoEntity> AtualizarMovimentacaoAsync(MovimentacaoEntity movimentacao);
    Task<MovimentacaoEntity> DeletarMovimentacaoAsync(int id);
}