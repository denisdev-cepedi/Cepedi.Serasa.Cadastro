using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class ObterConsultaRequest : IRequest<Result<ObterConsultaResponse>>
{
    public int Id { get; set; }
}