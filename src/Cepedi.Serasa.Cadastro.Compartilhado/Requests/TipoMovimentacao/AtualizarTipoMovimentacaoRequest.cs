﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
public class AtualizarTipoMovimentacaoRequest : IRequest<Result<AtualizarTipoMovimentacaoResponse>>, IValida
{
    public int Id { get; set; }
    public string NomeTipo { get; set; } = default!;
}