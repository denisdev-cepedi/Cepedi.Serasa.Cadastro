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
        try
        {
            var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.MovimentacaoId);

            if (movimentacaoEntity == null)
            {
                return Result.Error<AtualizarMovimentacaoResponse>(new ExcecaoAplicacao(new ResultadoErro
                {
                    Titulo = "Movimentação não encontrada",
                    Descricao = $"A movimentação com o ID {request.MovimentacaoId} não foi encontrada.",
                    Tipo = ETipoErro.Erro
                }));
            }

            // Atualizar propriedades da movimentação com base nos dados da requisição
            movimentacaoEntity.DataHora = request.DataHora;
            movimentacaoEntity.TipoMovimentacaoId = request.TipoMovimentacaoId;
            movimentacaoEntity.Valor = request.Valor;
            movimentacaoEntity.NomeEstabelecimento = request.NomeEstabelecimento;

            await _movimentacaoRepository.AtualizarMovimentacaoAsync(movimentacaoEntity);

            return Result.Success(new AtualizarMovimentacaoResponse(movimentacaoEntity.MovimentacaoId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro durante a execução ao atualizar a movimentação.");

            var excecaoAplicacao = new ExcecaoAplicacao(new ResultadoErro
            {
                Titulo = "Erro de Atualização de Movimentação",
                Descricao = "Ocorreu um erro ao atualizar a movimentação.",
                Tipo = ETipoErro.Erro
            });

            return Result.Error<AtualizarMovimentacaoResponse>(excecaoAplicacao);
        }
    }
}
