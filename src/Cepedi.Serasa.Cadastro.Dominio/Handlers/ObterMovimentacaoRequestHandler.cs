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
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;
public class ObterMovimentacaoRequestHandler : IRequestHandler<ObterMovimentacaoRequest, Result<ObterMovimentacaoResponse>>
{
    private readonly ILogger<ObterMovimentacaoRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public ObterMovimentacaoRequestHandler(IMovimentacaoRepository movimentacaoRepository, ILogger<ObterMovimentacaoRequestHandler> logger)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<ObterMovimentacaoResponse>> Handle(ObterMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacao = await _movimentacaoRepository.ObterMovimentacaoAsync(request.MovimentacaoId);

            if (movimentacao == null)
            {
                var erro = new ResultadoErro
                {
                    Titulo = "Movimentação não encontrada",
                    Descricao = $"Não foi encontrada uma movimentação com o ID {request.MovimentacaoId}.",
                    Tipo = ETipoErro.Erro
                };

                return Result.Error<ObterMovimentacaoResponse>(new ExcecaoAplicacao(erro));
            }

            // Transformar a entidade em uma resposta adequada
            var movimentacaoResponse = new ObterMovimentacaoResponse(movimentacao.MovimentacaoId, movimentacao.Valor);
            return Result.Success(movimentacaoResponse);
    }
}

