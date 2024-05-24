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

public class CriarScoreRequestHandlerTests
{
    private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<CriarScoreRequestHandler> _logger = Substitute.For<ILogger<CriarScoreRequestHandler>>();
    private readonly CriarScoreRequestHandler _sut;

    public CriarScoreRequestHandlerTests()
    {
        _sut = new CriarScoreRequestHandler(_scoreRepository, _logger, _pessoaRepository);
    }

    [Fact]
    public async Task Handle_QuandoCriarScore_DeveRetornarSucesso()
    {
        // Arrange
        var pessoa = new PessoaEntity
        {
            Id = 1,
            Nome = "João",
            CPF = "12345678901"
        };

        var request = new CriarScoreRequest
        {
            IdPessoa = pessoa.Id,
            Score = 750
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).Returns(Task.FromResult(pessoa));
        _scoreRepository.ObterPessoaScoreAsync(request.IdPessoa).Returns(Task.FromResult<ScoreEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarScoreResponse>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.IdPessoa.Should().Be(request.IdPessoa);
        result.Value.Score.Should().Be(request.Score);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _scoreRepository.Received(1).ObterPessoaScoreAsync(request.IdPessoa);
        await _scoreRepository.Received(1).CriarScoreAsync(Arg.Is<ScoreEntity>(s => s.IdPessoa == request.IdPessoa && s.Score == request.Score));
    }

    [Fact]
    public async Task Handle_QuandoPessoaNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var pessoaId = 999; // ID inválido que não existe no repositório
        var request = new CriarScoreRequest
        {
            IdPessoa = pessoaId,
            Score = 750
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).Returns(Task.FromResult<PessoaEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarScoreResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
            .Which.ResultadoErro.Should().Be(CadastroErros.IdPessoaInvalido);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _scoreRepository.DidNotReceive().ObterPessoaScoreAsync(Arg.Any<int>());
        await _scoreRepository.DidNotReceiveWithAnyArgs().CriarScoreAsync(Arg.Any<ScoreEntity>());
    }

    [Fact]
    public async Task Handle_QuandoScoreJaExistir_DeveRetornarErro()
    {
        // Arrange
        var pessoa = new PessoaEntity
        {
            Id = 1,
            Nome = "João",
            CPF = "12345678901"
        };

        var scoreEntity = new ScoreEntity
        {
            Id = 1,
            IdPessoa = pessoa.Id,
            Score = 800
        };

        var request = new CriarScoreRequest
        {
            IdPessoa = pessoa.Id,
            Score = 750
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).Returns(Task.FromResult(pessoa));
        _scoreRepository.ObterPessoaScoreAsync(request.IdPessoa).Returns(Task.FromResult(scoreEntity));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarScoreResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
            .Which.ResultadoErro.Should().Be(CadastroErros.ScoreJaExistente);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _scoreRepository.Received(1).ObterPessoaScoreAsync(request.IdPessoa);
        await _scoreRepository.DidNotReceiveWithAnyArgs().CriarScoreAsync(Arg.Any<ScoreEntity>());
    }
}

