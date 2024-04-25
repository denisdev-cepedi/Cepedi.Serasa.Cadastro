﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class DeletarTipoMovimentacaoRequest : IRequest<Result<DeletarTipoMovimentacaoResponse>>
{
    public int Id { get; set; }
}