using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;

public class DeletarMovimentacaoRequestHandler : IRequestHandler<DeletarMovimentacaoRequest, Result<DeletarMovimentacaoResponse>>
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<DeletarMovimentacaoRequestHandler> _logger;
    public DeletarMovimentacaoRequestHandler(ILogger<DeletarMovimentacaoRequestHandler> logger, IMovimentacaoRepository MovimentacaoRepository)
    {
        _logger = logger;
        _movimentacaoRepository = MovimentacaoRepository;
    }
    public async Task<Result<DeletarMovimentacaoResponse>> Handle(DeletarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);
        
        if (movimentacaoEntity == null) return Result.Error<DeletarMovimentacaoResponse>(new Compartilhado.Exececoes.SemResultadoExcecao());
        
        await _movimentacaoRepository.DeletarMovimentacaoAsync(movimentacaoEntity.Id);

        var response = new DeletarMovimentacaoResponse(
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