using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
public class ObterMovimentacaoRequestHandler : IRequestHandler<ObterMovimentacaoRequest, Result<ObterMovimentacaoResponse>>
{
    private readonly ILogger<ObterMovimentacaoRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public ObterMovimentacaoRequestHandler(ILogger<ObterMovimentacaoRequestHandler> logger, IMovimentacaoRepository movimentacaoRepository)
    {
        _logger = logger;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<Result<ObterMovimentacaoResponse>> Handle(ObterMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        // Verificar se a movimentação existe com base no ID fornecido na solicitação
        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);

        // Se a movimentação não existe, retornar um erro indicando a falta de resultados
        if (movimentacaoEntity == null)
        {
            return Result.Error<ObterMovimentacaoResponse>(
                new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.IdMovimentacaoInvalido));
        }

        // Criar uma resposta com os dados da movimentação obtida
        var response = new ObterMovimentacaoResponse(
            movimentacaoEntity.Id,
            movimentacaoEntity.IdTipoMovimentacao,
            movimentacaoEntity.IdPessoa,
            movimentacaoEntity.DataHora,
            movimentacaoEntity.NomeEstabelecimento,
            movimentacaoEntity.Valor
        );

        // Retornar um resultado de sucesso contendo a resposta da movimentação obtida
        return Result.Success(response);
    }
}
