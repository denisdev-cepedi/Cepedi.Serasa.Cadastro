using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class AtualizarMovimentacaoRequest : IRequest<Result<AtualizarMovimentacaoResponse>>
{
    public int MovimentacaoId { get; set; }
    public DateTime DataHora { get; set; }
    public int TipoMovimentacaoId { get; set; }
    public decimal Valor { get; set; }
    public string NomeEstabelecimento { get; set; } = string.Empty;
}

