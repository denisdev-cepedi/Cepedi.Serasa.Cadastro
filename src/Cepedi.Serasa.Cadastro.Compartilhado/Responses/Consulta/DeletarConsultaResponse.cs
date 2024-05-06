namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;

public record DeletarConsultaResponse(int id, int idPessoa, bool status, DateTime data);