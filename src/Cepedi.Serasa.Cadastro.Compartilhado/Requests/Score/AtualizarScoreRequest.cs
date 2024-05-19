using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
public class AtualizarScoreRequest : IRequest<Result<AtualizarScoreResponse>>, IValida
{
    public int Id { get; set; }
    public double Score { get; set; }
}
