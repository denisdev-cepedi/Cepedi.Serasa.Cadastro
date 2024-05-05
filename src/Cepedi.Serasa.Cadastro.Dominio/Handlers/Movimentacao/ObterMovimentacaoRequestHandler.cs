using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
public class ObterMovimentacaoRequestHandler : IRequestHandler<ObterMovimentacaoRequest, Result<ObterMovimentacaoResponse>>
{
    private readonly ILogger<ObterMovimentacaoRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    public ObterMovimentacaoRequestHandler(ILogger<ObterMovimentacaoRequestHandler> logger, IMovimentacaoRepository MovimentacaoRepository){
        _logger = logger;
        _movimentacaoRepository = MovimentacaoRepository;
    }
    public async Task<Result<ObterMovimentacaoResponse>> Handle(ObterMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);
        
        return movimentacaoEntity == null
            ? Result.Error<ObterMovimentacaoResponse>(new Compartilhado.Exececoes.SemResultadoExcecao())
            : Result.Success(new ObterMovimentacaoResponse(movimentacaoEntity.Id, movimentacaoEntity.TipoMovimentacaoId, movimentacaoEntity.DataHora, movimentacaoEntity.NomeEstabelecimento, movimentacaoEntity.Valor));
    }
}