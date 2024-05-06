namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;

public record ObterConsultaResponse(int id, int idPessoa, bool status, DateTime data);