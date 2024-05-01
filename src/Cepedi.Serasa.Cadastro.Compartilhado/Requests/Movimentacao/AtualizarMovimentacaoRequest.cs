using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;

public class AtualizarMovimentacaoRequest : IRequest<Result<AtualizarMovimentacaoResponse>>
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int TipoMovimentacaoId { get; set; }
    public string NomeEstabelecimento { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}

