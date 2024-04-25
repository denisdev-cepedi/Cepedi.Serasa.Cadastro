﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class AtualizarTipoMovimentacaoRequest : IRequest<Result<AtualizarTipoMovimentacaoResponse>>
{
    public int Id { get; set; }
    public string NomeTipo { get; set; } = default!;
}