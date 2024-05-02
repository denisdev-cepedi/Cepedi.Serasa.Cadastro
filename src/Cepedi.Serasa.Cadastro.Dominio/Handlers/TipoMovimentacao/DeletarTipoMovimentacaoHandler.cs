﻿using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;

public class DeletarTipoMovimentacaoRequestHandler : IRequestHandler<DeletarTipoMovimentacaoRequest, Result<DeletarTipoMovimentacaoResponse>>
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<DeletarTipoMovimentacaoRequestHandler> _logger;
    public DeletarTipoMovimentacaoRequestHandler(ILogger<DeletarTipoMovimentacaoRequestHandler> logger, ITipoMovimentacaoRepository tipoMovimentacaoRepository)
    {
        _logger = logger;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
    }
    public async Task<Result<DeletarTipoMovimentacaoResponse>> Handle(DeletarTipoMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var tipoMovimentacaoEntity = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id);
        if (tipoMovimentacaoEntity == null) return Result.Error<DeletarTipoMovimentacaoResponse>(new Compartilhado.Exececoes.SemResultadoExcecao());
        await _tipoMovimentacaoRepository.DeletarTipoMovimentacaoAsync(tipoMovimentacaoEntity.Id);
        return Result.Success(new DeletarTipoMovimentacaoResponse(tipoMovimentacaoEntity.Id));
    }
}