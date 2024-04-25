using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Repository;
public interface IMovimentacaoRepository
{
    Task<MovimentacaoEntity?> ObterMovimentacaoAsync(int movimentacaoId);

    Task<MovimentacaoEntity> CriarMovimentacaoAsync(MovimentacaoEntity movimentacao);

    Task<MovimentacaoEntity> AtualizarMovimentacaoAsync(MovimentacaoEntity movimentacao);

    Task DeletarMovimentacaoAsync(int movimentacaoId);
}
