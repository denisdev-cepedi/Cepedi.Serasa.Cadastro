using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests
{
    public class DeletarMovimentacaoRequest : IRequest<Result<DeletarMovimentacaoResponse>>
    {
        public int MovimentacaoId { get; set; }
    }
}
