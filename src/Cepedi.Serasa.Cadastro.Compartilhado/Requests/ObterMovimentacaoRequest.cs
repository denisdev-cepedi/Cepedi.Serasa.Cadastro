using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests;
public class ObterMovimentacaoRequest : IRequest<Result<ObterMovimentacaoResponse>>
{
    public int MovimentacaoId { get; set; }

    public ObterMovimentacaoRequest(int movimentacaoId)
    {
        MovimentacaoId = movimentacaoId;
    }
}

