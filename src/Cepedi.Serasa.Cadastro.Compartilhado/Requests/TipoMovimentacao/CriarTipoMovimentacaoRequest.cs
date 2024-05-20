using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
public class CriarTipoMovimentacaoRequest : IRequest<Result<CriarTipoMovimentacaoResponse>>, IValida
{
    public string NomeTipo { get; set; } = default!;
}