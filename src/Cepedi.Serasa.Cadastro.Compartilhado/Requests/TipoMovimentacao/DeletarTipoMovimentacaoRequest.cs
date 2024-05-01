﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
public class DeletarTipoMovimentacaoRequest : IRequest<Result<DeletarTipoMovimentacaoResponse>>
{
    public int Id { get; set; }
}