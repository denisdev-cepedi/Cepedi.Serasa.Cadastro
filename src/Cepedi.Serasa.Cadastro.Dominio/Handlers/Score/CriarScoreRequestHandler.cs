using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Score;
public class CriarScoreRequestHandler
    : IRequestHandler<CriarScoreRequest, Result<CriarScoreResponse>>
{
    private readonly ILogger<CriarScoreRequestHandler> _logger;
    private readonly IScoreRepository _scoreRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public CriarScoreRequestHandler(IScoreRepository scoreRepository, ILogger<CriarScoreRequestHandler> logger, IPessoaRepository pessoaRepository)
    {
        _scoreRepository = scoreRepository;
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<CriarScoreResponse>> Handle(CriarScoreRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaRepository.ObterPessoaAsync(request.IdPessoa);
        if (pessoa == null)
        {
            return Result.Error<CriarScoreResponse>(
                new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.IdPessoaInvalido));
        }

        var credorEntity = await _scoreRepository.ObterPessoaScoreAsync(request.IdPessoa);

        var score = new ScoreEntity()
        {
            Score = request.Score,

            IdPessoa = request.IdPessoa,
        };

        await _scoreRepository.CriarScoreAsync(score);

        return Result.Success(new CriarScoreResponse(score.Id, score.IdPessoa, score.Score));
    }
}
