using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
public class CriarConsultaRequest : IRequest<Result<CriarConsultaResponse>>
{
    public required DateTime Data { get; set; }
    public required bool Status { get; set; }
    public int IdPessoa { get; set; }
}
