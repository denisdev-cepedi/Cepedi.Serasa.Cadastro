using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
public class CriarScoreRequest : IRequest<Result<CriarScoreResponse>>
{
    public int IdPessoa { get; set; }
    public double Score { get; set; }
}