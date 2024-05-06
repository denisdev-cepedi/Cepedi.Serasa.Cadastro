using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
public class ObterScoreRequest : IRequest<Result<ObterScoreResponse>>
{
    public int Id { get; set; }
}