namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
public record AtualizarMovimentacaoResponse(int tipoMovimentacaoId, DateTime dataHora, string? NomeEstabelecimento, decimal valor);

