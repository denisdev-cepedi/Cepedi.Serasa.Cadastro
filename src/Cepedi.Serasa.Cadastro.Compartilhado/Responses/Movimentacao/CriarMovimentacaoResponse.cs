namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
public record CriarMovimentacaoResponse(int Id, int TipoMovimentacaoId, decimal Valor, DateTime DataHora, string NomeEstabelecimento);

