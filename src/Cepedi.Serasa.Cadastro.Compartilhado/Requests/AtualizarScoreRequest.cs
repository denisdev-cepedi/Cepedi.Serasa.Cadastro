using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;
namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class AtualizarScoreRequest : IRequest<Result<AtualizarScoreResponse>>
{
    public int Id { get; set; }
    public double Score { get; set; }
    public int IdPessoa { get; set; }
}
