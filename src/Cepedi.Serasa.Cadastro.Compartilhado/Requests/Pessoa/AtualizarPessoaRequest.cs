using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
public class AtualizarPessoaRequest : IRequest<Result<AtualizarPessoaResponse>>
{
    public required int Id { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
}
