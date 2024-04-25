using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;
public class ExcluirPessoaPorIdRequest : IRequest<Result<ObterPessoaResponse>>
{
    public int Id { get; set; }
}
