﻿using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;

public class AtualizarTipoMovimentacaoRequestHandler : IRequestHandler<AtualizarTipoMovimentacaoRequest, Result<AtualizarTipoMovimentacaoResponse>>
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<AtualizarTipoMovimentacaoRequestHandler> _logger;
    public AtualizarTipoMovimentacaoRequestHandler(ITipoMovimentacaoRepository tipoMovimentacaoRepository, ILogger<AtualizarTipoMovimentacaoRequestHandler> logger)
    {
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
        _logger = logger;
    }
    public async Task<Result<AtualizarTipoMovimentacaoResponse>> Handle(AtualizarTipoMovimentacaoRequest request, CancellationToken cancellationToken)
    {

        var tipoMovimentacaoEntity = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id);

        if (tipoMovimentacaoEntity == null)
        {
            return Result.Error<AtualizarTipoMovimentacaoResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        tipoMovimentacaoEntity.Atualizar(request.NomeTipo);

        await _tipoMovimentacaoRepository.AtualizarTipoMovimentacaoAsync(tipoMovimentacaoEntity);

        return Result.Success(new AtualizarTipoMovimentacaoResponse(tipoMovimentacaoEntity.NomeTipo));
    }
}