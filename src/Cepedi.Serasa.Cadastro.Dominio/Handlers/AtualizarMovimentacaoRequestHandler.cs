using System;
using System.Threading;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repository;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Compartilhado.Excecoes;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;
public class AtualizarMovimentacaoRequestHandler : IRequestHandler<AtualizarMovimentacaoRequest, Result<AtualizarMovimentacaoResponse>>
{
    private readonly ILogger<AtualizarMovimentacaoRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public AtualizarMovimentacaoRequestHandler(IMovimentacaoRepository movimentacaoRepository, ILogger<AtualizarMovimentacaoRequestHandler> logger)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarMovimentacaoResponse>> Handle(AtualizarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.MovimentacaoId);

        if (movimentacaoEntity == null)
        {
            return Result.Error<AtualizarMovimentacaoResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        movimentacaoEntity.Atualizar(request.Valor);

        await _movimentacaoRepository.AtualizarMovimentacaoAsync(movimentacaoEntity);

        return Result.Success(new AtualizarMovimentacaoResponse(movimentacaoEntity.Valor));
    }
}
