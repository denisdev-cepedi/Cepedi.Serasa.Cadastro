using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class DeletarScoreRequest : IRequest<Result<DeletarScoreResponse>>
{
    public int Id { get; set; }
}
