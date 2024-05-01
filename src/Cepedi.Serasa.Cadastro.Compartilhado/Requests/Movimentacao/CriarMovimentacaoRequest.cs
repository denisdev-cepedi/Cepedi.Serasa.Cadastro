using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;

public class CriarMovimentacaoRequest : IRequest<Result<CriarMovimentacaoResponse>>
{
    public int PessoaId { get; set; }

    public DateTime DataHora { get; set; }

    public int TipoMovimentacaoId { get; set; }

    public decimal Valor { get; set; }

    public string NomeEstabelecimento { get; set; } = default!;
}

