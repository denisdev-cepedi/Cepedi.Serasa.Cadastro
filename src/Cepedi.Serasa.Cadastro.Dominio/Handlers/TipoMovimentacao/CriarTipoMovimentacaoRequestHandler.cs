using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;
public class CriarTipoMovimentacaoRequestHandler : IRequestHandler<CriarTipoMovimentacaoRequest, Result<CriarTipoMovimentacaoResponse>>
{
    private readonly ILogger<CriarTipoMovimentacaoRequestHandler> _logger;
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;

    public CriarTipoMovimentacaoRequestHandler(ILogger<CriarTipoMovimentacaoRequestHandler> logger, ITipoMovimentacaoRepository tipoMovimentacaoRepository)
    {
        _logger = logger;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
    }
    public async Task<Result<CriarTipoMovimentacaoResponse>> Handle(CriarTipoMovimentacaoRequest request, CancellationToken cancellationToken)
    {

        var tipoMovimentacao = new TipoMovimentacaoEntity()
        {
            NomeTipo = request.NomeTipo,
        };

        await _tipoMovimentacaoRepository.CriarTipoMovimentacaoAsync(tipoMovimentacao);
        return Result.Success(new CriarTipoMovimentacaoResponse(tipoMovimentacao.Id, tipoMovimentacao.NomeTipo));

    }
}