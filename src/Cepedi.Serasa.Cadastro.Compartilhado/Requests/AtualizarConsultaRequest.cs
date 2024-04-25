using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;
public class AtualizarConsultaRequest : IRequest<Result<AtualizarConsultaResponse>>
{
    public int Id { get; set; }
    public int IdPessoa { get; set; }
    public DateTime Data { get; set; }
    public bool Status { get; set; }

}
