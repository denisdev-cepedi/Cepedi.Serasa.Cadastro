using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;
public class CriarScoreRequest : IRequest<Result<CriarScoreResponse>>
{
    public double Score { get; set; }
    public int IdPessoa { get; set; }
}