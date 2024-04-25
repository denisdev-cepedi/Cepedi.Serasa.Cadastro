using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;
public class DeletarConsultaRequest : IRequest<Result<DeletarConsultaResponse>>
{
    public int Id { get; set; }
}
