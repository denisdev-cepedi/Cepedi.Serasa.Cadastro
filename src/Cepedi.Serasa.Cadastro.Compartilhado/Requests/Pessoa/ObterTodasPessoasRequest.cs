using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
public class ObterTodasPessoasRequest : IRequest<Result<IEnumerable<ObterPessoaResponse>>>
{
}
