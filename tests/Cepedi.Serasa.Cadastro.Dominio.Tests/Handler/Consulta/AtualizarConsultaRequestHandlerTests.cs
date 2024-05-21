using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Consulta;
public class AtualizarConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly ILogger<AtualizarConsultaRequestHandler> _logger = Substitute.For<ILogger<AtualizarConsultaRequestHandler>>();
    private readonly AtualizarConsultaRequestHandler _sut;

    public AtualizarConsultaRequestHandlerTests()
    {
        _sut = new AtualizarConsultaRequestHandler(_consultaRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoAtualizarConsulta_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarConsultaRequest
        {
            Id = 1,
            Status = true
        };

        var consultaExistente = new ConsultaEntity
        {
            Id = request.Id,
            IdPessoa = 1,
            Status = false,
            Data = DateTime.UtcNow
        };

        _consultaRepository.ObterConsultaAsync(request.Id).Returns(Task.FromResult(consultaExistente));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarConsultaResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(consultaExistente.Id);
        result.Value.IdPessoa.Should().Be(consultaExistente.IdPessoa);
        result.Value.Status.Should().Be(request.Status);
        result.Value.Data.Should().Be(consultaExistente.Data);

        await _consultaRepository.Received(1).ObterConsultaAsync(request.Id);
        await _consultaRepository.Received(1).AtualizarConsultaAsync(Arg.Is<ConsultaEntity>(
            c => c.Id == request.Id &&
                    c.Status == request.Status
        ));
    }

    [Fact]
    public async Task Handle_QuandoConsultaNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarConsultaRequest
        {
            Id = 1,
            Status = true
        };

        _consultaRepository.ObterConsultaAsync(request.Id).Returns(Task.FromResult<ConsultaEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarConsultaResponse>>()
                .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
                .Which.ResultadoErro.Should().Be(CadastroErros.IdConsultaInvalido);
    }
}

