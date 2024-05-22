using Cepedi.Serasa.Cadastro.Dominio.Handlers.Score;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Score;

public class DeletarScoreRequestHandlerTests
{
    private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
    private readonly ILogger<DeletarScoreRequestHandler> _logger = Substitute.For<ILogger<DeletarScoreRequestHandler>>();
    private readonly DeletarScoreRequestHandler _sut;

    public DeletarScoreRequestHandlerTests()
    {
        _sut = new DeletarScoreRequestHandler(_scoreRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoDeletarScore_DeveRetornarSucesso()
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

        // Act
        var result = await _sut.Handle(new DeletarScoreRequest { Id = idScore }, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarScoreResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        // Verificar se o método no repositório foi chamado corretamente
        await _scoreRepository.Received(1).ObterScoreAsync(idScore);
        await _scoreRepository.Received(1).DeletarScoreAsync(idScore);
    }

    [Fact]
    public async Task Handle_QuandoDeletarScoreInexistente_DeveRetornarFalha()
    {
        // Arrange
        var idScoreInexistente = 99;

        _scoreRepository.ObterScoreAsync(idScoreInexistente).Returns(Task.FromResult<ScoreEntity>(null));

        // Act
        var result = await _sut.Handle(new DeletarScoreRequest { Id = idScoreInexistente }, CancellationToken.None);

        // Assert
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.IsSuccess.Should().BeFalse(); // Verifica se a operação falhou
    }
}

