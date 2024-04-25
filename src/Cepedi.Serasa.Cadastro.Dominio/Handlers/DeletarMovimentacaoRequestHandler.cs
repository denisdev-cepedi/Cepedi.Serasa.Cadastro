using Cepedi.Serasa.Cadastro.Dominio.Repository;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;
public class DeletarMovimentacaoRequestHandler : IRequestHandler<DeletarMovimentacaoRequest, Result<DeletarMovimentacaoResponse>>
{
    private readonly ILogger<DeletarMovimentacaoRequestHandler> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public DeletarMovimentacaoRequestHandler(IMovimentacaoRepository movimentacaoRepository, ILogger<DeletarMovimentacaoRequestHandler> logger)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarMovimentacaoResponse>> Handle(DeletarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
       var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.MovimentacaoId);

        if (movimentacaoEntity == null)
        {
            return Result.Error<DeletarMovimentacaoResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        await _movimentacaoRepository.DeletarMovimentacaoAsync(movimentacaoEntity.MovimentacaoId);

        return Result.Success(new DeletarMovimentacaoResponse(movimentacaoEntity.MovimentacaoId));
    }
}
