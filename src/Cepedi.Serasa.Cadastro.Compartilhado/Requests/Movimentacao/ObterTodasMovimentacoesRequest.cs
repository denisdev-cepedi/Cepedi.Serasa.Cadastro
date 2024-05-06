using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao
{
    public class ObterTodasMovimentacoesRequest : IRequest<Result<List<ObterTodasMovimentacoesResponse>>>
    {
    }
}
