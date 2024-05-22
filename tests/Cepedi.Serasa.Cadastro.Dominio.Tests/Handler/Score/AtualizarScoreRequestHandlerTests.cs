using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Score;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Score;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Score;

public class AtualizarScoreRequestHandlerTests
{
    private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
    private readonly ILogger<AtualizarScoreRequestHandler> _logger = Substitute.For<ILogger<AtualizarScoreRequestHandler>>();
    private readonly AtualizarScoreRequestHandler _sut;

    public AtualizarScoreRequestHandlerTests()
    {
        _sut = new AtualizarScoreRequestHandler(_scoreRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoAtualizarScore_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarScoreRequest
        {
            Id = 1,
            Score = 800
        };

        var scoreEntity = new ScoreEntity
        {
            Id = request.Id,
            Score = 750
        };

        _scoreRepository.ObterScoreAsync(request.Id).Returns(Task.FromResult(scoreEntity));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarScoreResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(scoreEntity.Id);
        result.Value.Score.Should().Be(request.Score);

        await _scoreRepository.Received(1).ObterScoreAsync(request.Id);
        await _scoreRepository.Received(1).AtualizarScoreAsync(Arg.Is<ScoreEntity>(
            s => s.Id == request.Id && s.Score == request.Score
        ));
    }

    [Fact]
    public async Task Handle_QuandoScoreNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarScoreRequest
        {
            Id = 1,
            Score = 800
        };

        _scoreRepository.ObterScoreAsync(request.Id).Returns(Task.FromResult<ScoreEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarScoreResponse>>()
                .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
                .Which.ResultadoErro.Should().Be(CadastroErros.IdScoreInvalido);
    }
}
