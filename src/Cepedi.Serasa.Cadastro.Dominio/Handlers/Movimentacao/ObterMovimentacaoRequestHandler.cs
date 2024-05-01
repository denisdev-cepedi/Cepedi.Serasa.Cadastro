using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
public class ObterMovimentacaoRequestHandler : IRequestHandler<ObterMovimentacaoRequest, Result<ObterMovimentacaoResponse>>
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<ObterMovimentacaoRequestHandler> _logger;

    public ObterMovimentacaoRequestHandler(IMovimentacaoRepository movimentacaoRepository, ILogger<ObterMovimentacaoRequestHandler> logger)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<ObterMovimentacaoResponse>> Handle(ObterMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacao = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);

        return movimentacao == null
            ? Result.Error<ObterMovimentacaoResponse>(new SemResultadoExcecao())
            : Result.Success(new ObterMovimentacaoResponse(movimentacao.Id, movimentacao.TipoMovimentacaoId, movimentacao.DataHora, movimentacao.NomeEstabelecimento, movimentacao.Valor));
    }
}
