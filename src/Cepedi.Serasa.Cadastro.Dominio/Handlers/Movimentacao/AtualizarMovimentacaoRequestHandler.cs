using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao;
public class AtualizarMovimentacaoRequestHandler
    : IRequestHandler<AtualizarMovimentacaoRequest, Result<AtualizarMovimentacaoResponse>>
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<AtualizarMovimentacaoRequestHandler> _logger;

    public AtualizarMovimentacaoRequestHandler(IMovimentacaoRepository MovimentacaoRepository, ILogger<AtualizarMovimentacaoRequestHandler> logger)
    {
        _movimentacaoRepository = MovimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarMovimentacaoResponse>> Handle(AtualizarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacao = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);

        if (movimentacao == null)
        {
            return Result.Error<AtualizarMovimentacaoResponse>(new SemResultadoExcecao());
        }

        movimentacao.Atualizar(request.TipoMovimentacaoId, request.DataHora, request.NomeEstabelecimento, request.Valor);
        await _movimentacaoRepository.AtualizarMovimentacaoAsync(movimentacao);

        return Result.Success(new AtualizarMovimentacaoResponse(movimentacao.TipoMovimentacaoId, movimentacao.DataHora, movimentacao.NomeEstabelecimento, movimentacao.Valor));
    }
}
