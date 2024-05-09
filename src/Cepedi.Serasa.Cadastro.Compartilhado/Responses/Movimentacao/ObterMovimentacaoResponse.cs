namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
public record ObterMovimentacaoResponse(int Id, int IdTipoMovimentacao, int IdPessoa, DateTime DataHora, string? NomeEstabelecimento, decimal Valor);

