namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
public record ObterTodasMovimentacoesResponse(int Id, int IdTipoMovimentacao, int IdPessoa, DateTime DataHora, string? NomeEstabelecimento, decimal Valor);

