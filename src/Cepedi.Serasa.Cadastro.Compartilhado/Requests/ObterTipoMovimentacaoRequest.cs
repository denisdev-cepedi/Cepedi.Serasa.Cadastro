﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;

public class ObterTipoMovimentacaoRequest : IRequest<Result<ObterTipoMovimentacaoResponse>>
{
    public int Id { get; set; }
}