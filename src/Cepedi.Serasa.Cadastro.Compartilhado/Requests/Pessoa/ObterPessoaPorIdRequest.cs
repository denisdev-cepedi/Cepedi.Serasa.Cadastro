using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
public class ObterPessoaPorIdRequest : IRequest<Result<ObterPessoaResponse>>
{
    public int Id { get; set; }
}
