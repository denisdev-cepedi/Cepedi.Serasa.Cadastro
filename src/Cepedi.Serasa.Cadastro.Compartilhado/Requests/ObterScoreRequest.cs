using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class ObterScoreRequest : IRequest<Result<ObterScoreResponse>>
{
    public int Id { get; set; }
}