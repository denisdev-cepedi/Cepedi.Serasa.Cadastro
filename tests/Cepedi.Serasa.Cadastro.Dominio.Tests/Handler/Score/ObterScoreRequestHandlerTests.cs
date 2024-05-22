using Cepedi.Serasa.Cadastro.Dominio.Handlers.Score;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Score;

public class ObterScoreRequestHandlerTests
{
    private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
    private readonly ILogger<ObterScoreRequestHandler> _logger = Substitute.For<ILogger<ObterScoreRequestHandler>>();
    private readonly ObterScoreRequestHandler _sut;

    public ObterScoreRequestHandlerTests()
    {
        _sut = new ObterScoreRequestHandler(_scoreRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoObterScoreExistente_DeveRetornarScore()
    {
        // Arrange
        var idScore = 1;

        var scoreExistente = new ScoreEntity
        {
            Id = idScore,
            IdPessoa = 1,
            Score = 750
        };

        _scoreRepository.ObterScoreAsync(idScore).Returns(Task.FromResult(scoreExistente));

        var request = new ObterScoreRequest { Id = idScore };

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        result.Value.Id.Should().Be(scoreExistente.Id);
        result.Value.IdPessoa.Should().Be(scoreExistente.IdPessoa);
        result.Value.Score.Should().Be(scoreExistente.Score);

        // Verifica se o método no repositório foi chamado corretamente
        await _scoreRepository.Received(1).ObterScoreAsync(idScore);
    }

    [Fact]
    public async Task Handle_QuandoObterScoreInexistente_DeveRetornarErro()
    {
        // Arrange
        var idScoreInexistente = 99;

        _scoreRepository.ObterScoreAsync(idScoreInexistente).Returns(Task.FromResult<ScoreEntity>(null));

        var request = new ObterScoreRequest { Id = idScoreInexistente };

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeNull();

        // Verifica se o método no repositório foi chamado corretamente
        await _scoreRepository.Received(1).ObterScoreAsync(idScoreInexistente);
    }
}

