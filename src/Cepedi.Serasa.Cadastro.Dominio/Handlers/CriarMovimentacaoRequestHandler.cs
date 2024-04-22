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
public class CriarMovimentacaoRequestHandler : IRequestHandler<CriarMovimentacaoRequest, Result<CriarMovimentacaoResponse>>
{
    private readonly ILogger<CriarMovimentacaoRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public CriarMovimentacaoRequestHandler(IMovimentacaoRepository movimentacaoRepository, ILogger<CriarMovimentacaoRequestHandler> logger)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<CriarMovimentacaoResponse>> Handle(CriarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var movimentacao = new MovimentacaoEntity()
            {
                PessoaId = request.PessoaId,
                DataHora = request.DataHora,
                TipoMovimentacaoId = request.TipoMovimentacaoId,
                Valor = request.Valor,
                NomeEstabelecimento = request.NomeEstabelecimento
            };

            await _movimentacaoRepository.CriarMovimentacaoAsync(movimentacao);

            return Result.Success(new CriarMovimentacaoResponse(movimentacao.MovimentacaoId, movimentacao.Valor));
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu um erro durante a execução ao criar a movimentação: {Message}", ex.Message);

            // Construir e retornar a exceção aplicação conforme o padrão exigido
            var excecaoAplicacao = new ExcecaoAplicacao(new ResultadoErro
            {
                Titulo = "Erro de Gravação de Movimentação",
                Descricao = "Ocorreu um erro ao gravar a movimentação.",
                Tipo = ETipoErro.Erro, // Definir o tipo de erro conforme necessário
            });

            return Result.Error<CriarMovimentacaoResponse>(excecaoAplicacao);
        }
    }
}
