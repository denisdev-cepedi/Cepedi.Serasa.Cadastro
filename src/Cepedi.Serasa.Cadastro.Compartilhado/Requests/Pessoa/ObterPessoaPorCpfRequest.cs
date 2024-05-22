using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
public class ObterPessoaPorCpfRequest : IRequest<Result<IEnumerable<ObterPessoaResponse>>>
{
    public string Cpf { get; set; }
}
