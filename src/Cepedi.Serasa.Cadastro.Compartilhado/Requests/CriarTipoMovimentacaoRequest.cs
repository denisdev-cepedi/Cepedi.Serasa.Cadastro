using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class CriarTipoMovimentacaoRequest : IRequest<Result<CriarTipoMovimentacaoResponse>>
{
    public string NomeTipo { get; set; } = default!;
}