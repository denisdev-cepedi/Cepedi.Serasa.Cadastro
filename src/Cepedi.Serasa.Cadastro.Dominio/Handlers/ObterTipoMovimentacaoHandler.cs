﻿using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio;

public class ObterTipoMovimentacaoRequestHandler : IRequestHandler<ObterTipoMovimentacaoRequest, Result<ObterTipoMovimentacaoResponse>>
{
    private readonly ILogger<ObterTipoMovimentacaoRequestHandler> _logger;
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    public ObterTipoMovimentacaoRequestHandler(ILogger<ObterTipoMovimentacaoRequestHandler> logger, ITipoMovimentacaoRepository tipoMovimentacaoRepository){
        _logger = logger;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
    }
    public async Task<Result<ObterTipoMovimentacaoResponse>> Handle(ObterTipoMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var tipoMovimentacaoEntity = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id);
        return tipoMovimentacaoEntity == null
            ? Result.Error<ObterTipoMovimentacaoResponse>(new Compartilhado.Exececoes.SemResultadoExcecao())
            : Result.Success(new ObterTipoMovimentacaoResponse(tipoMovimentacaoEntity.NomeTipo));
    }
}