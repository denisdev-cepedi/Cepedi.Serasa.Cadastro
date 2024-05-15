using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao
{
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
            var movimentacoes = await _movimentacaoRepository.ObterTodasMovimentacoesAsync();

            if (movimentacoes == null)
            {
                // Retorna uma lista vazia se não houver movimentações
                _logger.LogInformation("Nenhuma movimentação encontrada.");
                return Result.Success(new List<ObterTodasMovimentacoesResponse>());
            }

            var response = new List<ObterTodasMovimentacoesResponse>();
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

            return Result.Success(response);
        }
    }
}
