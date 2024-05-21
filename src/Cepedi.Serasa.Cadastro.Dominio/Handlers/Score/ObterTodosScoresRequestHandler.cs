using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Score;
public class ObterTodosScoresRequestHandler : IRequestHandler<ObterTodosScoresRequest, Result<List<ObterTodosScoresResponse>>>
{
    private readonly ILogger<ObterTodosScoresRequestHandler> _logger;
    private readonly IScoreRepository _scoreRepository;

    public ObterTodosScoresRequestHandler(ILogger<ObterTodosScoresRequestHandler> logger, IScoreRepository scoreRepository)
    {
        _logger = logger;
        _scoreRepository = scoreRepository;
    }

    public async Task<Result<List<ObterTodosScoresResponse>>> Handle(ObterTodosScoresRequest request, CancellationToken cancellationToken)
    {
        var scores = await _scoreRepository.ObterTodosScoresAsync();

        if (scores == null)
        {
            return Result.Error<List<ObterTodosScoresResponse>>(new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.ListaScoresVazia));
        }

        var response = new List<ObterTodosScoresResponse>();
        foreach (var score in scores)
        {
            response.Add(new ObterTodosScoresResponse(score.Id, score.IdPessoa, score.Score));
        }

        return Result.Success(response);
    }
}
