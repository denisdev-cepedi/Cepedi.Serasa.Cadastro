using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
public class CriarPessoaRequest : IRequest<Result<CriarPessoaResponse>>
{
    public required string Nome { get; set; }
    public required string CPF { get; set; }
}
