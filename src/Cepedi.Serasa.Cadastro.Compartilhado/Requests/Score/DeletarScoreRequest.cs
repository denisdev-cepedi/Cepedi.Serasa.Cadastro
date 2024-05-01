using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
public class DeletarScoreRequest : IRequest<Result<DeletarScoreResponse>>
{
    public int Id { get; set; }
}
