namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;

public record DeletarConsultaResponse(int Id, int IdPessoa, bool Status, DateTime Data);