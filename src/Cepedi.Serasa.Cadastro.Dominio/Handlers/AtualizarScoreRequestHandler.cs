using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;
public class AtualizarScoreRequestHandler :
    IRequestHandler<AtualizarScoreRequest, Result<AtualizarScoreResponse>>
{
    private readonly IScoreRepository _scoreRepository;
    private readonly ILogger<AtualizarScoreRequestHandler> _logger;

    public AtualizarScoreRequestHandler(IScoreRepository scoreRepository, ILogger<AtualizarScoreRequestHandler> logger)
    {
        _scoreRepository = scoreRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarScoreResponse>> Handle(AtualizarScoreRequest request, CancellationToken cancellationToken)
    {
        var scoreEntity = await _scoreRepository.ObterScoreAsync(request.Id);

        if (scoreEntity == null)
        {
            return Result.Error<AtualizarScoreResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        scoreEntity.Atualizar(request.Score);

        await _scoreRepository.AtualizarScoreAsync(scoreEntity);

        return Result.Success(new AtualizarScoreResponse(scoreEntity.Score));
    }
}
