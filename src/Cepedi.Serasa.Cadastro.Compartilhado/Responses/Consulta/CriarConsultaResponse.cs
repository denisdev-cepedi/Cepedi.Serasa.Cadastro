﻿namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;

public record CriarConsultaResponse(int id, int idPessoa, bool status, DateTime data);