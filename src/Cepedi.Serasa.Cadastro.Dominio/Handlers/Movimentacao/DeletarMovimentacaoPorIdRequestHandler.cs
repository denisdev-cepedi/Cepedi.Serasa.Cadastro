using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao;
internal class DeletarMovimentacaoRequestHandler : IRequestHandler<DeletarMovimentacaoRequest, Result<ObterMovimentacaoResponse>>
{
    private readonly IMovimentacaoRepository _MovimentacaoRepository;
    private readonly ILogger<DeletarMovimentacaoRequestHandler> _logger;

    public DeletarMovimentacaoRequestHandler(IMovimentacaoRepository MovimentacaoRepository, ILogger<DeletarMovimentacaoRequestHandler> logger)
    {
        _MovimentacaoRepository = MovimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<ObterMovimentacaoResponse>> Handle(DeletarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacao = await _MovimentacaoRepository.ObterMovimentacaoAsync(request.Id);

        if (movimentacao == null)
        {
            return Result.Error<ObterMovimentacaoResponse>(new SemResultadoExcecao());
        }

        await _MovimentacaoRepository.DeletarMovimentacaoAsync(movimentacao);

        return Result.Success(new ObterMovimentacaoResponse(movimentacao.Id, movimentacao.TipoMovimentacaoId, movimentacao.DataHora, movimentacao.NomeEstabelecimento, movimentacao.Valor));
    }
}
