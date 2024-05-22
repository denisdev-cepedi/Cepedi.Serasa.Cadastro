using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Score;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Score;

public class ObterTodosScoresRequestHandlerTests
{
    private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
    private readonly ILogger<ObterTodosScoresRequestHandler> _logger = Substitute.For<ILogger<ObterTodosScoresRequestHandler>>();
    private readonly ObterTodosScoresRequestHandler _sut;

    public ObterTodosScoresRequestHandlerTests()
    {
        _sut = new ObterTodosScoresRequestHandler(_logger, _scoreRepository);
    }

    [Fact]
    public async Task Handle_QuandoObterTodosScores_DeveRetornarListaScores()
    {
        // Arrange
        var scores = new List<ScoreEntity>
        {
            new ScoreEntity { Id = 1, IdPessoa = 1, Score = 750 },
            new ScoreEntity { Id = 2, IdPessoa = 2, Score = 800 }
        };

        _scoreRepository.ObterTodosScoresAsync().Returns(Task.FromResult(scores));

        var request = new ObterTodosScoresRequest();

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        // Verifica se as listas têm o mesmo número de elementos
        result.Value.Should().HaveCount(scores.Count);

        // Verifica se cada elemento na lista resultante corresponde ao elemento correspondente na lista original
        for (int i = 0; i < scores.Count; i++)
        {
            result.Value[i].Id.Should().Be(scores[i].Id);
            result.Value[i].IdPessoa.Should().Be(scores[i].IdPessoa);
            result.Value[i].Score.Should().Be(scores[i].Score);
        }

        // Verifica se o método no repositório foi chamado corretamente
        await _scoreRepository.Received(1).ObterTodosScoresAsync();
    }

    [Fact]
    public async Task Handle_QuandoNaoExistemScores_DeveRetornarErro()
    {
        // Arrange
        _scoreRepository.ObterTodosScoresAsync().Returns(Task.FromResult<List<ScoreEntity>>(null));

        var request = new ObterTodosScoresRequest();

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeNull();

        // Verifica se o método no repositório foi chamado corretamente
        await _scoreRepository.Received(1).ObterTodosScoresAsync();
    }
}

