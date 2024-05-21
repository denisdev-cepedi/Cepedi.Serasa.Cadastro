﻿using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;

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
            return Result.Error<AtualizarTipoMovimentacaoResponse>(
                new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.IdTipoMovimentacaoInvalido));
        }

        tipoMovimentacaoEntity.Atualizar(request.NomeTipo);

        await _tipoMovimentacaoRepository.AtualizarTipoMovimentacaoAsync(tipoMovimentacaoEntity);

        return Result.Success(new AtualizarTipoMovimentacaoResponse(tipoMovimentacaoEntity.Id, tipoMovimentacaoEntity.NomeTipo));
    }
}