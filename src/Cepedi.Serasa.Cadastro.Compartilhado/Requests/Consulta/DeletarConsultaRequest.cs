using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
public class DeletarConsultaRequest : IRequest<Result<DeletarConsultaResponse>>
{
    public int Id { get; set; }
}
