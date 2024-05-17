using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
public class ObterTodasMovimentacoesRequestHandler : IRequestHandler<ObterTodasMovimentacoesRequest, Result<List<ObterTodasMovimentacoesResponse>>>
{
    private readonly ILogger<ObterTodasMovimentacoesRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public ObterTodasMovimentacoesRequestHandler(ILogger<ObterTodasMovimentacoesRequestHandler> logger, IMovimentacaoRepository movimentacaoRepository)
    {
        _logger = logger;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<Result<List<ObterTodasMovimentacoesResponse>>> Handle(ObterTodasMovimentacoesRequest request, CancellationToken cancellationToken)
    {
        // Obter todas as movimentações do repositório
        var movimentacoes = await _movimentacaoRepository.ObterTodasMovimentacoesAsync();

        // Verificar se a lista de movimentações é nula
        if (movimentacoes == null)
        {
            // Se não houver movimentações, retornar um erro indicando a falta de resultados
            return Result.Error<List<ObterTodasMovimentacoesResponse>>(
                new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.ListaMovimentacoesVazia));
        }

        // Inicializar uma lista para armazenar as respostas das movimentações
        var response = new List<ObterTodasMovimentacoesResponse>();

        // Para cada movimentação obtida, criar uma resposta correspondente
        foreach (var movimentacao in movimentacoes)
        {
            response.Add(new ObterTodasMovimentacoesResponse(
                movimentacao.Id,
                movimentacao.IdTipoMovimentacao,
                movimentacao.IdPessoa,
                movimentacao.DataHora,
                movimentacao.NomeEstabelecimento,
                movimentacao.Valor));
        }

        // Retornar um resultado de sucesso contendo a lista de respostas das movimentações
        return Result.Success(response);
    }
}

