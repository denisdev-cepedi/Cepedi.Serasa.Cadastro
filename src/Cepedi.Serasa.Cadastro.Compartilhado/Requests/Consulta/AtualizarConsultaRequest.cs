using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
public class AtualizarConsultaRequest : IRequest<Result<AtualizarConsultaResponse>>
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public bool Status { get; set; }

}
