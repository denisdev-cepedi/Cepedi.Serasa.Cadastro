using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao;
public class ObterTodasMovimentacoesRequestHandler : IRequestHandler<ObterTodasMovimentacoesRequest, Result<IEnumerable<ObterMovimentacaoResponse>>>
{
    private readonly IMovimentacaoRepository _movimentacoesRepository;
    private readonly ILogger<ObterTodasMovimentacoesRequestHandler> _logger;

    public ObterTodasMovimentacoesRequestHandler(IMovimentacaoRepository movimentacoesRepository, ILogger<ObterTodasMovimentacoesRequestHandler> logger)
    {
        _movimentacoesRepository = movimentacoesRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ObterMovimentacaoResponse>>> Handle(ObterTodasMovimentacoesRequest request, CancellationToken cancellationToken)
    {
        var movimentacoes = await _movimentacoesRepository.ObterMovimentacoesAsync();

        return !movimentacoes.Any()
            ? Result.Error<IEnumerable<ObterMovimentacaoResponse>>(new SemResultadoExcecao())
            : Result.Success(movimentacoes.Select(movimentacao => new ObterMovimentacaoResponse(movimentacao.Id, movimentacao.TipoMovimentacaoId, movimentacao.DataHora, movimentacao.NomeEstabelecimento, movimentacao.Valor)));
    }
}
