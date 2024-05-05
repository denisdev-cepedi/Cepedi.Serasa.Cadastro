﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao
{
    public class ObterTodosTiposMovimentacaoRequest : IRequest<Result<List<ObterTodosTiposMovimentacaoResponse>>>
    {
    }
}
