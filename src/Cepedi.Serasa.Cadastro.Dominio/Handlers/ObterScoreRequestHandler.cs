using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;
public class ObterScoreRequestHandler :
    IRequestHandler<ObterScoreRequest, Result<ObterScoreResponse>>
{
    private readonly IScoreRepository _scoreRepository;
    private readonly ILogger<ObterScoreRequestHandler> _logger;

    public ObterScoreRequestHandler(IScoreRepository scoreRepository, ILogger<ObterScoreRequestHandler> logger)
    {
        _scoreRepository = scoreRepository;
        _logger = logger;
    }

    public async Task<Result<ObterScoreResponse>> Handle(ObterScoreRequest request, CancellationToken cancellationToken)
    {
        var scoreEntity = await _scoreRepository.ObterScoreAsync(request.Id);

        if (scoreEntity == null)
        {
            return Result.Error<ObterScoreResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        return Result.Success(new ObterScoreResponse(scoreEntity.Score));

    }
}
