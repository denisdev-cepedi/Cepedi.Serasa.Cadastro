using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
public class CriarMovimentacaoRequestHandler : IRequestHandler<CriarMovimentacaoRequest, Result<CriarMovimentacaoResponse>>
{
    private readonly ILogger<CriarMovimentacaoRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public CriarMovimentacaoRequestHandler(ILogger<CriarMovimentacaoRequestHandler> logger, IMovimentacaoRepository movimentacaoRepository)
    {
        _logger = logger;
        _movimentacaoRepository = movimentacaoRepository;
    }
    public async Task<Result<CriarMovimentacaoResponse>> Handle(CriarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacao = new MovimentacaoEntity()
        {
            TipoMovimentacaoId = request.TipoMovimentacaoId,
            Valor = request.Valor,
            DataHora = DateTime.UtcNow,
            NomeEstabelecimento = request.NomeEstabelecimento
        };


        await _movimentacaoRepository.CriarMovimentacaoAsync(movimentacao);

        var response = new CriarMovimentacaoResponse(
            movimentacao.Id,
            movimentacao.TipoMovimentacaoId,
            movimentacao.Valor,
            movimentacao.DataHora,
            movimentacao.NomeEstabelecimento
        );

        return Result.Success(response);

    }
}