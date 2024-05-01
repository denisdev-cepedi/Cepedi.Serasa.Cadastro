using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
public class ObterTodasMovimentacoesRequest : IRequest<Result<IEnumerable<ObterMovimentacaoResponse>>>
{
}
