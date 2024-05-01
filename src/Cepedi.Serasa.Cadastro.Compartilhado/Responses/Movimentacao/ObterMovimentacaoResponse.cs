namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
public record ObterMovimentacaoResponse(int Id, int TipoMovimentacaoId, DateTime DataHora, string? NomeEstabelecimento, decimal Valor);

