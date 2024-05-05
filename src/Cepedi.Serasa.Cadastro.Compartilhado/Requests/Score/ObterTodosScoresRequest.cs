﻿using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score
{
    public class ObterTodosScoresRequest : IRequest<Result<List<ObterTodosScoresResponse>>>
    {
    }
}
