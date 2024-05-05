namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
public record AtualizarMovimentacaoResponse(int Id, int IdTipoMovimentacao, DateTime DataHora, string? NomeEstabelecimento, decimal Valor);

