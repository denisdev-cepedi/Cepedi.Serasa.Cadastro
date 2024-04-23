using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;
public class ObterTodasPessoasRequest : IRequest<Result<IEnumerable<ObterPessoaResponse>>>
{
}
