using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
public class CriarScoreRequest : IRequest<Result<CriarScoreResponse>>
{
    public double Score { get; set; }
    public int IdPessoa { get; set; }
}