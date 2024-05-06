using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Score;
public class DeletarScoreRequestHandler :
    IRequestHandler<DeletarScoreRequest, Result<DeletarScoreResponse>>
{
    private readonly IScoreRepository _scoreRepository;
    private readonly ILogger<DeletarScoreRequestHandler> _logger;

    public DeletarScoreRequestHandler(IScoreRepository scoreRepository, ILogger<DeletarScoreRequestHandler> logger)
    {
        _scoreRepository = scoreRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarScoreResponse>> Handle(DeletarScoreRequest request, CancellationToken cancellationToken)
    {
        var scoreEntity = await _scoreRepository.ObterScoreAsync(request.Id);

        if (scoreEntity == null)
        {
            return Result.Error<DeletarScoreResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        await _scoreRepository.DeletarScoreAsync(scoreEntity.Id);

        return Result.Success(new DeletarScoreResponse(scoreEntity.Id, scoreEntity.IdPessoa, scoreEntity.Score));
    }
}
