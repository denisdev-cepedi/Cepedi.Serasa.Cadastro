using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;

public class AtualizarMovimentacaoRequestHandler : IRequestHandler<AtualizarMovimentacaoRequest, Result<AtualizarMovimentacaoResponse>>
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<AtualizarMovimentacaoRequestHandler> _logger;
    public AtualizarMovimentacaoRequestHandler(IMovimentacaoRepository movimentacaoRepository, ILogger<AtualizarMovimentacaoRequestHandler> logger)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }
    public async Task<Result<AtualizarMovimentacaoResponse>> Handle(AtualizarMovimentacaoRequest request, CancellationToken cancellationToken)
    {

        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);

        if (movimentacaoEntity == null)
        {
            return Result.Error<AtualizarMovimentacaoResponse>(new Compartilhado.Exececoes.SemResultadoExcecao());
        }

        movimentacaoEntity.Atualizar(
            request.IdTipoMovimentacao,
            request.DataHora,
            request.NomeEstabelecimento,
            request.Valor
        );

        await _movimentacaoRepository.AtualizarMovimentacaoAsync(movimentacaoEntity);

        var response = new AtualizarMovimentacaoResponse(
            movimentacaoEntity.Id,
            movimentacaoEntity.IdTipoMovimentacao,
            movimentacaoEntity.IdPessoa,
            movimentacaoEntity.DataHora,
            movimentacaoEntity.NomeEstabelecimento,
            movimentacaoEntity.Valor
        );

        return Result.Success(response);
    }
}