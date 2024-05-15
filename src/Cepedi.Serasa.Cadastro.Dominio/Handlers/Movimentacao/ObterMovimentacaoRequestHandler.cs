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

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao
{
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
            var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);

            if (movimentacaoEntity == null)
            {
                _logger.LogInformation($"Movimentacao com Id {request.Id} não encontrada.");
                return Result.Error<ObterMovimentacaoResponse>(new SemResultadoExcecao());
            }

            var response = new ObterMovimentacaoResponse(
                movimentacaoEntity.Id,
                movimentacaoEntity.IdTipoMovimentacao,
                movimentacaoEntity.IdPessoa,
                movimentacaoEntity.DataHora,
                movimentacaoEntity.NomeEstabelecimento,
                movimentacaoEntity.Valor
            );

            return Result.Success(response);
        }
    }
}
