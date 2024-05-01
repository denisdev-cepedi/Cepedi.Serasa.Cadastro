﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
public class ObterTipoMovimentacaoRequest : IRequest<Result<ObterTipoMovimentacaoResponse>>
{
    public int Id { get; set; }
}